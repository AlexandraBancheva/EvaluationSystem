using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IFormModuleRepository : IRepository<FormModule>
    {
        void AddNewModuleInForm(int formId, int moduleId, int position);

        void DeleteModuleFromForm(int formId, int moduleId);

        ICollection<FormModule> GetAllModulesByFormId(int formId);

        ICollection<ModuleTemplate> GetModulesByFormId(int formId);
    }
}