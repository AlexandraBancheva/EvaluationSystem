using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Services
{
    public class ModulesServices : IModulesServices
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModulesServices(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }

        public ModuleDetailDto CreateModule(CreateModuleDto model)
        {
            var currentEntity = _mapper.Map<ModuleTemplate>(model);
            _moduleRepository.Insert(currentEntity);

            return _mapper.Map<ModuleDetailDto>(currentEntity);
        }

        public void DeleteCurrentModule(int moduleId)
        {
            var entity = _moduleRepository.GetById(moduleId);
            _moduleRepository.Delete(entity);
        }

        public ModuleDetailDto GetModuleById(int moduleId)
        {
            var entity = _moduleRepository.GetById(moduleId);

            return _mapper.Map<ModuleDetailDto>(entity);
        }

        public ModuleDetailDto UpdateCurrentModule(int moduleId, UpdateModuleDto model)
        {
            var entity = _mapper.Map<ModuleTemplate>(model);
            entity.Id = moduleId;
            _moduleRepository.Update(entity);

            return _mapper.Map<ModuleDetailDto>(entity);
        }
    }
}
