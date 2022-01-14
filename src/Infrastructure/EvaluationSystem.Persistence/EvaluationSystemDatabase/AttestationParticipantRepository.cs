using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;
using System.Collections.Generic;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationParticipantRepository : BaseRepository<AttestationParticipant>, IAttestationParticipantRepository
    {
        public AttestationParticipantRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteAttestationIdFromAttestationParticipant(int userId, int attestationId)
        {
            var query = @"DELETE FROM [AttestationParticipant]
                        WHERE IdUserParticipant = @UserId
                        DELETE FROM [AttestationParticipant]
                        WHERE  IdAttestation = @AttestationId";

            Connection.Execute(query, new { UserId = userId, AttestationId = attestationId }, Transaction);
        }

        public ICollection<AttestationParticipant> GetAllAttestationParticipant(int userId)
        {
            var query = @"SELECT * FROM AttestationParticipant
                        WHERE IdUserParticipant = @UserId";

            var res = Connection.Query<AttestationParticipant>(query, new { UserId = userId }, Transaction);

            return (ICollection<AttestationParticipant>)res;
        }
    }
}
