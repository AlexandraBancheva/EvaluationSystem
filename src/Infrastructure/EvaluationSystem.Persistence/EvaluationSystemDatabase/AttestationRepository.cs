using Dapper;
using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Application.Models.Participants;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;
using System.Collections.Generic;
using System.Linq;

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

            _connection.Execute(query, new { IdAttestation  = attestationId, IdUserParticipant = participantId, Status = status, Position = position }, _transaction);
        }

        public void DeleteAttestation(int attestationId)
        {
            var query = @"DELETE FROM [AttestationParticipant]
                        WHERE IdAttestation = @IdAttestation
                        DELETE FROM [Attestation]
                        WHERE Id = @IdAttestation";

            _connection.Execute(query, new { IdAttestation = attestationId }, _transaction);
        }

        public ICollection<AttestationInfoDbDto> GetAllAttestation()
        {
            var query = @"SELECT * FROM [Attestation] AS [at]
                        LEFT JOIN [User] AS u ON u.IdUser = [at].IdUserToEvaluate
                        LEFT JOIN AttestationForm AS af ON [at].IdForm = af.Id
                        LEFT JOIN AttestationParticipant AS ap ON ap.IdAttestation = [at].Id
                        RIGHT JOIN [User] AS up ON up.IdUser = ap.IdUserParticipant";

            var attestationDictionary = new Dictionary<int, AttestationInfoDbDto>();
            var attestations = _connection.Query<AttestationInfoDbDto, ParticipantsInfoDbDto, AttestationInfoDbDto>(query, (attest, participant) =>
            {
                if (attestationDictionary.TryGetValue(attest.Id, out var currentAttest))
                {
                    currentAttest = attest;
                    attestationDictionary.Add(currentAttest.Id, currentAttest);
                }

                currentAttest.Participants.Add(participant);
                return currentAttest;
            }, _transaction, splitOn: "Id")
                .Distinct()
                .ToList();

            return attestations;
        }
    }
}
