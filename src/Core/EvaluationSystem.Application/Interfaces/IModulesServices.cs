using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IModulesServices
    {
        void DeleteCurrentModule(int moduleId);

        CurrentModuleDetailDto CreateModule(int formId, CreateModuleDto model);

        CurrentModuleDetailDto UpdateCurrentModule(int formId, int moduleId, UpdateModuleDto model);

        CurrentModuleDetailDto GetCurrentModuleById(int formId, int moduleId);

        ICollection<ListFormIdWithAllModulesDto> GetAllModulesByFormId(int formId);
    }
}