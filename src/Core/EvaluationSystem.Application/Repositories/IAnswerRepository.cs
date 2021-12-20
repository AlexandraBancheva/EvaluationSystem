using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswerRepository : IRepository<AnswerTemplate>
    {
        ICollection<AnswerTemplate> GetAllByQuestionId(int questionId);
    }
}
