using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Services
{
    public class AttestationModulesServices : IAttestationModulesServices
    {
        private readonly IAttestationModuleRepository _attestationModuleRepository;
        private readonly IAttestationFormModulesServices _attestationFormModulesServices;
        private IMapper _mapper;

        public AttestationModulesServices(IAttestationModuleRepository attestationModuleRepository, IAttestationFormModulesServices attestationFormModulesServices, IMapper mapper)
        {
            _attestationModuleRepository = attestationModuleRepository;
            _attestationFormModulesServices = attestationFormModulesServices;
            _mapper = mapper;
        }

        public void CreateModule(int formId, CreateModuleDto model)
        {
            var currentEntity = _mapper.Map<AttestationModule>(model);
            var newEntityId = _attestationModuleRepository.Insert(currentEntity);

            _attestationFormModulesServices.AddModuleInForm(formId, newEntityId, model.Position);
        }

        public void DeleteCurrentModule(int moduleId)
        {
            _attestationModuleRepository.DeleteAttestatationModule(moduleId);
        }
    }
}