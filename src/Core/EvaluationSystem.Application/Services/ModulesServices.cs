﻿using AutoMapper;
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
        private readonly IFormModulesServices _formModulesServices;
        private readonly IMapper _mapper;

        public ModulesServices(IModuleRepository moduleRepository, IFormModulesServices formModulesServices, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _formModulesServices = formModulesServices;
            _mapper = mapper;
        }

        public ModuleDetailDto CreateModule(int formId, int position, CreateModuleDto model)
        {
            var currentEntity = _mapper.Map<ModuleTemplate>(model);
            var newEntityId = _moduleRepository.Insert(currentEntity);
           _formModulesServices.AddModulesInForm(formId, newEntityId, position);


            return GetCurrentModuleById(formId, newEntityId);
        }

        public void DeleteCurrentModule(int moduleId)
        {
            _moduleRepository.DeleteModule(moduleId);
        }

        public ModuleDetailDto GetCurrentModuleById(int formId, int moduleId)
        {
            var entity = _moduleRepository.GetModuleById(formId, moduleId);

            return _mapper.Map<ModuleDetailDto>(entity);
        }

        public ModuleDetailDto UpdateCurrentModule(int formId, int moduleId, UpdateModuleDto model)
        {
            var entity = _mapper.Map<ModuleTemplate>(model);
            entity.Id = moduleId;
            _moduleRepository.UpdateModule(formId, moduleId, entity);

            return GetCurrentModuleById(formId, moduleId);
        }
    }
}
