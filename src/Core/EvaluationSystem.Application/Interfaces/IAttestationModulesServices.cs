using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationModulesServices
    {
        void CreateModule(int formId, CreateModuleDto model);

        void DeleteCurrentModule(int moduleId);
    }
}
