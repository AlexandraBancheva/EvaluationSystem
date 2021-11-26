using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IModuleRepository : IRepository<ModuleTemplate>
    {
        void DeleteModule(int moduleId);

        ModuleTemplate GetModuleById(int formId, int moduleId);

        void UpdateModule(int formId, int moduleId, ModuleTemplate module);
    }
}