using System.Collections.Generic;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IModulesServices
    {
        CurrentModuleDetailDto CreateModule(int formId, CreateModuleDto model);

        void DeleteCurrentModule(int moduleId);

        CurrentModuleDetailDto UpdateCurrentModule(int formId, int moduleId, UpdateModuleDto model);

        CurrentModuleDetailDto GetCurrentModuleById(int formId, int moduleId);

       ICollection<ListFormIdWithAllModulesDto> GetAllModulesByFormId(int formId);
    }
}