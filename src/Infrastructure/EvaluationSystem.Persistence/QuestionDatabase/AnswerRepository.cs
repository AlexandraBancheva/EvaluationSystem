using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class AnswerRepository : BaseRepository<AnswerTemplate>, IAnswerRepository
    {
        //public AnswerRepository(IConfiguration configuration) 
        //    : base(configuration)
        //{
        //}

        public AnswerRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

    }
}
