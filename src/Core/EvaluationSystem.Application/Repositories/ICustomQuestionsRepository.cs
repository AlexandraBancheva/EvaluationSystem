using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface ICustomQuestionsRepository : IRepository<QuestionTemplate>
    {
        void RemovedQuestion(int questionId);

        void DeleteCustomQuestion(int questionId);
    }
}