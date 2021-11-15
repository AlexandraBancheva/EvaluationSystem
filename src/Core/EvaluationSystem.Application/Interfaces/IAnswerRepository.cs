using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswerRepository
    {
        void AddNewAnswer(int questionId, AnswerTemplate model);

        void DeleteAnAnswer(int answerId);
    }
}
