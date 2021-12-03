using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IModulesServices
    {
        ModuleDetailDto CreateModule(int formId, int position, CreateModuleDto model);

        void DeleteCurrentModule(int moduleId);

        ModuleDetailDto UpdateCurrentModule(int formId, int moduleId, UpdateModuleDto model);

        ModuleDetailDto GetCurrentModuleById(int formId, int moduleId);

       ICollection<ModuleDetailDto> GetAllModulesByFormId(int formId);
    }
}