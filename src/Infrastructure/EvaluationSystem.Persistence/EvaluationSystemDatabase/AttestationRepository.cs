using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.QuestionDatabase;
using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Application.Models.Participants;

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
            Connection.Execute(query, new { IdAttestation  = attestationId, IdUserParticipant = participantId, Status = status, Position = position }, transaction: Transaction);
        }

        public void DeleteAttestation(int attestationId)
        {
            var query = @"DELETE FROM [AttestationParticipant]
                        WHERE IdAttestation = @IdAttestation
                        DELETE FROM [Attestation]
                        WHERE Id = @IdAttestation";

            var queryParameter = new { IdAttestation = attestationId };
            Connection.Execute(query, queryParameter, transaction: Transaction);
        }

        public ICollection<Attestation> GetAllAtestationByUserId(int userId)
        {
            var query = @"SELECT * FROM [Attestation]
                        WHERE IdUserToEvaluate = @UserId";

            var queryParameter = new { UserId = userId };
            var res = Connection.Query<Attestation>(query, queryParameter, transaction: Transaction);

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
            }, transaction: Transaction, splitOn: "IdUser")
                .Distinct()
                .ToList();

            return attestations;
        }
    }
}