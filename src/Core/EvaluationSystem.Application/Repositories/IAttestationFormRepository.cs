using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationFormRepository : IRepository<AttestationForm>
    {
        void DeleteAttestationForm(int formId);
    }
}
