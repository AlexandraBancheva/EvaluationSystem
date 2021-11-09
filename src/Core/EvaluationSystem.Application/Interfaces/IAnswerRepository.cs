using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswerRepository
    {
        void AddNewAnswer(int questionId, Answer model);

        void DeleteAnAnswer(int answerId);
    }
}
