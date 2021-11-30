using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface ICustomQuestionsRepository : IRepository<QuestionTemplate>
    {
        void DeleteQuestion(int questionId);
    }
}
