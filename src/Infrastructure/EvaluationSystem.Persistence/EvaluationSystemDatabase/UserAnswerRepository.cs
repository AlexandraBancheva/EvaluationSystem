using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void ChangeStatusToDone(int attestationId, int idUserParticipant)
        {
            var query = @"UPDATE AttestationParticipant
                        SET [Status] = 'Done'
                        WHERE IdAttestation = @AttestationId AND IdUserParticipant = @IdUserParticipant";

            Connection.Execute(query, new { AttestationId = attestationId, IdUserParticipant = idUserParticipant }, transaction: Transaction);
        }

        public void DeleteUserAnswerByAttestationId(int attestationId)
        {
            var query = @"DELETE [UserAnswer]
                        WHERE IdAttestation = @AttestationId";

            Connection.Execute(query, new { AttestationId = attestationId }, transaction: Transaction);
        }

        public ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId)
        {
            var query = @"SELECT * FROM [UserAnswer] AS ua
                        JOIN [AttestationParticipant] AS ap ON ua.IdAttestation = ap.IdAttestation
                        WHERE ua.IdAttestation = @IdAttestation AND ap.IdUserParticipant = @IdUserParticipant  AND ap.[Status] = 'Done'";

            var results = Connection.Query<UserAnswer>(query, new { IdAttestation = attestationId, IdUserParticipant = userId }, transaction: Transaction);

            return (ICollection<UserAnswer>)results;
        }

        public UserAnswer GetUserAnswerByAttestationId(int attestationId)
        {
            var query = @"SELECT * FROM [UserAnswer]
                        WHERE IdAttestation = @AttestationId";

            var queryParameter = new { AttestationId = attestationId };

            var res = Connection.QueryFirstOrDefault<UserAnswer>(query, queryParameter, transaction: Transaction);

            return res;
        }
    }
}