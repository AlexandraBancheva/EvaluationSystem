using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationModuleRepository : BaseRepository<AttestationModule>, IAttestationModuleRepository
    {
        public AttestationModuleRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}