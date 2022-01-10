using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Services
{
    public class AttestationFormModulesServices : IAttestationFormModulesServices
    {
        private readonly IAttestationFormModuleRepository _attestationFormModuleRepository;

        public AttestationFormModulesServices(IAttestationFormModuleRepository attestationFormModuleRepository)
        {
            _attestationFormModuleRepository = attestationFormModuleRepository;
        }

        public void AddModuleInForm(int formId, int moduleId, int position)
        {
            _attestationFormModuleRepository.AddModuleInForm(formId, moduleId, position);
        }
    }
}
