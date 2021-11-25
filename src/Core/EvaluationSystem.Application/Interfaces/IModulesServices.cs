using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IModulesServices
    {
        ModuleDetailDto CreateModule(CreateModuleDto model);

        void DeleteCurrentModule(int moduleId);

        ModuleDetailDto UpdateCurrentModule(int moduleId, UpdateModuleDto model);

        ModuleDetailDto GetModuleById(int moduleId);
    }
}