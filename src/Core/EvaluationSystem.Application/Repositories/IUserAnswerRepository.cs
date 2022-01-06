using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserAnswerRepository : IRepository<UserAnswer>
    {
        void ChangeStatusToDone(int attestationId, int idUserParticipant);
    }
}
