﻿using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.FormModules;

namespace EvaluationSystem.Application.Services
{
    public class ModulesServices : IModulesServices
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IFormModuleRepository _formModuleRepository;
        private readonly IFormModulesServices _formModulesServices;
        private readonly IModuleQuestionsServices _moduleQuestionsServices;
        private readonly IMapper _mapper;

        public ModulesServices(IModuleRepository moduleRepository, 
                                IFormModuleRepository formModuleRepository,
                                IFormModulesServices formModulesServices,
                                IModuleQuestionsServices moduleQuestionsServices, 
                                IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _formModuleRepository = formModuleRepository;
            _formModulesServices = formModulesServices;
            _moduleQuestionsServices = moduleQuestionsServices;
            _mapper = mapper;
        }

        public CurrentModuleDetailDto CreateModule(int formId, CreateModuleDto model)
        {
            var currentEntity = _mapper.Map<ModuleTemplate>(model);
            var newEntityId = _moduleRepository.Insert(currentEntity);
            _formModulesServices.AddModulesInForm(formId, newEntityId, model.Position);
            var module = _mapper.Map<CurrentModuleDetailDto>(currentEntity);
            module.Id = newEntityId;
            module.Position = model.Position;
            return module;
        }

        public void DeleteCurrentModule(int moduleId)
        {
            _moduleRepository.DeleteModule(moduleId);
        }

        public CurrentModuleDetailDto GetCurrentModuleById(int formId, int moduleId)
        {
            var entity = _moduleRepository.GetModuleById(formId, moduleId);

            return _mapper.Map<CurrentModuleDetailDto>(entity);
        }

        public CurrentModuleDetailDto UpdateCurrentModule(int formId, int moduleId, UpdateModuleDto model)
        {
            var entity = _mapper.Map<ModuleTemplate>(model);
            entity.Id = moduleId;
            _moduleRepository.UpdateModule(formId, moduleId, entity);

            return GetCurrentModuleById(formId, moduleId);
        }

        public ICollection<ListFormIdWithAllModulesDto> GetAllModulesByFormId(int formId)
        {
            var allModules = _formModuleRepository.GetModulesByFormId(formId);
           
            return _mapper.Map<ICollection<ListFormIdWithAllModulesDto>>(allModules);
        }
    }
}
