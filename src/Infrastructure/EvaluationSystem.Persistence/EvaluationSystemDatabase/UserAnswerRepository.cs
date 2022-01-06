using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.QuestionDatabase;
using Dapper;

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
    }
}
