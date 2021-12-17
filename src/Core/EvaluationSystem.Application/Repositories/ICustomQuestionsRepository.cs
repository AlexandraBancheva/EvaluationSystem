using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Repositories
{
    public interface ICustomQuestionsRepository : IRepository<QuestionTemplate>
    {
        void RemovedQuestion(int questionId);

        void DeleteCustomQuestion(int questionId);

        QuestionTemplateDto GetCustomById(int questionId);
    }
}