using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswerRepository : IRepository<AnswerTemplate>
    {
        void InsertAnswer(int questionId, AddNewAnswerDto model);
    }
}
