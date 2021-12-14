﻿using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IModuleRepository : IRepository<ModuleTemplate>
    {
        void DeleteModule(int moduleId);

        ModuleTemplateDto GetModuleById(int formId, int moduleId);

        void UpdateModule(int formId, int moduleId, ModuleTemplate module);
    }
}