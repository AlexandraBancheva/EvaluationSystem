using System.Collections.Generic;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository : IRepository<QuestionTemplate>
    {
        List<QuestionTemplate> GetAllQuestionsWithAnswers();
    }
}
