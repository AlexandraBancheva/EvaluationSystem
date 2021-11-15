using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository : IRepository<QuestionTemplate>
    {
        List<QuestionTemplate> GetAllQuestionsWithAnswers();
    }
}
