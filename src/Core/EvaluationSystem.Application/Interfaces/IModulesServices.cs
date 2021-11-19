using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IModulesServices
    {
        //Create module
        ModuleDetailDto CreateModule(CreateModuleDto model);

        //Delete module
        void DeleteCurrentModule(int moduleId);

        //GetAllWithQuestions

        //Update module
        ModuleDetailDto UpdateCurrentModule(int moduleId, UpdateModuleDto model);

        // Get module by Id
        ModuleDetailDto GetModuleById(int moduleId);
    }
}
