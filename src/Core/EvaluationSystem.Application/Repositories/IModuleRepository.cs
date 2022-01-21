using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Repositories
{
    public interface IModuleRepository : IRepository<ModuleTemplate>
    {
        void DeleteModule(int moduleId);

        void UpdateModule(int formId, int moduleId, ModuleTemplate module);

        ModuleTemplateDto GetModuleById(int formId, int moduleId);
    }
}