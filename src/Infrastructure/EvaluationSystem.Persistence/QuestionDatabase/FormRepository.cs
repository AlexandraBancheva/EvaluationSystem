using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class FormRepository : BaseRepository<FormTemplate>, IFormRepository
    {
        //public FormRepository(IConfiguration configuration)
        //   : base(configuration)
        //{
        //}
        public FormRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
