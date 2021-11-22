using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IModuleRepository : IRepository<ModuleTemplate>
    {
        void DeleteModule(int moduleId);
    }
}