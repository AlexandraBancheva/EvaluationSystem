using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository : IRepository<QuestionTemplate>
    {
        void DeleteTemplateQuestion(int questionId);

        ICollection<QuestionTemplate> GetAllQuestionsWithAnswers();

        ICollection<QuestionTemplate> GetAllById(int questionId);

        // 20.12
        ICollection<QuestionTemplate> GetAllQuestionTemplates();
    }
}