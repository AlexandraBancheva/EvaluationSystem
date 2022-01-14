using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Application.Models.Participants;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationRepository : BaseRepository<Attestation>, IAttestationRepository
    {
        public AttestationRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void AddUserParticipantsToAttestation(int attestationId, int participantId, string position)
        {
            var query = @"INSERT INTO [AttestationParticipant]
                        VALUES (@IdAttestation, @IdUserParticipant, @Status, @Position)";
            var status = "Open";

            Connection.Execute(query, new { IdAttestation  = attestationId, IdUserParticipant = participantId, Status = status, Position = position }, Transaction);
        }

        public void DeleteAttestation(int attestationId)
        {
            var query = @"DELETE FROM [AttestationParticipant]
                        WHERE IdAttestation = @IdAttestation
                        DELETE FROM [Attestation]
                        WHERE Id = @IdAttestation";

            Connection.Execute(query, new { IdAttestation = attestationId }, Transaction);
        }

        public ICollection<Attestation> GetAllAtestationByUserId(int userId)
        {
            var query = @"SELECT * FROM [Attestation]
                        WHERE IdUserToEvaluate = @UserId";

            var res = Connection.Query<Attestation>(query, new { UserId = userId}, Transaction);

            return (ICollection<Attestation>)res;
        }

        public ICollection<AttestationInfoDbDto> GetAllAttestation()
        {
            var query = @"SELECT [at].Id AS IdAttestation, [af].Id AS IdForm, u.[Name] AS Username, af.[Name] AS FormName, ap.[Status] AS Status, [at].CreateDate, up.Id AS IdUser, up.[Name] AS ParticipantUser, up.[Email] AS ParticipantEmail, ap.[Status] AS ParticipantStatus
                        FROM [Attestation] AS [at]
                        JOIN [User] AS u ON u.Id = [at].IdUserToEvaluate
                        JOIN AttestationForm AS af ON [at].IdForm = af.Id
                        JOIN AttestationParticipant AS ap ON ap.IdAttestation = [at].Id
                        JOIN [User] AS up ON up.Id = ap.IdUserParticipant";

            var attestationDictionary = new Dictionary<int, AttestationInfoDbDto>();
            var attestations = Connection.Query<AttestationInfoDbDto, ParticipantsInfoDbDto, AttestationInfoDbDto>(query, (attestation, participant) =>
            {
                if (!attestationDictionary.TryGetValue(attestation.IdAttestation, out var currentAttest))
                {
                    currentAttest = attestation;
                    attestationDictionary.Add(currentAttest.IdAttestation, currentAttest);
                }

                currentAttest.Participants.Add(participant);
                return currentAttest;
            }, Transaction, splitOn: "IdUser")
                .Distinct()
                .ToList();

            return attestations;
        }
    }
}