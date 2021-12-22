using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationQuestionRepository : BaseRepository<AttestationQuestion>
    {
        public AttestationQuestionRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}