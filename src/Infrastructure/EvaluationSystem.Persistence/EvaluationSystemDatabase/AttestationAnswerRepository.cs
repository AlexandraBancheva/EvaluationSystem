using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationAnswerRepository : BaseRepository<AttestationAnswer>, IAttestationAnswerRepository
    {
        public AttestationAnswerRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
