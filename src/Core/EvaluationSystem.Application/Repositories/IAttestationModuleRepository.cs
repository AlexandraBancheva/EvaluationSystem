using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationModuleRepository : IRepository<AttestationModule>
    {
        void DeleteAttestatationModule(int moduleId);
    }
}
