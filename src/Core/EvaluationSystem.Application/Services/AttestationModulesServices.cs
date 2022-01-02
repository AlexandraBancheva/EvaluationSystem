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
        private IMapper _mapper;

        public AttestationModulesServices(IAttestationModuleRepository attestationModuleRepository, IMapper mapper)
        {
            _attestationModuleRepository = attestationModuleRepository;
            _mapper = mapper;
        }

        public void CreateModule(int formId, CreateModuleDto model)
        {
            var currentEntity = _mapper.Map<AttestationModule>(model);
            var newEntityId = _attestationModuleRepository.Insert(currentEntity);

            // Add to linkage table!!!
           // _formModulesServices.AddModulesInForm(formId, newEntityId, model.Position);
        }

        public void DeleteCurrentModule(int moduleId)
        {
            _attestationModuleRepository.DeleteAttestatationModule(moduleId);
        }
    }
}