﻿using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IModulesServices
    {
        ModuleDetailDto CreateModule(CreateModuleDto model);

        void DeleteCurrentModule(int moduleId);

        ModuleDetailDto UpdateCurrentModule(int formId, int moduleId, UpdateModuleDto model);

        ModuleDetailDto GetCurrentModuleById(int formId, int moduleId);
    }
}