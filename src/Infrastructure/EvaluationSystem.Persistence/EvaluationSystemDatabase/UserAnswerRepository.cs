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

            _connection.Execute(query, new { AttestationId = attestationId, IdUserParticipant = idUserParticipant }, _transaction);
        }

        public ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId)
        {
            var query = @"SELECT * FROM [UserAnswer]
                        WHERE IdAttestation = @IdAttestation AND IdUserParticipant = @IdUserParticipant";

            var results = _connection.Query<UserAnswer>(query, new { IdAttestation = attestationId, IdUserParticipant = userId}, _transaction);

            return (ICollection<UserAnswer>)results;
        }
    }
}
