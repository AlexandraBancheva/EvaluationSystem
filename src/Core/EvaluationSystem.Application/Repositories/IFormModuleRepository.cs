using System.Collections.Generic;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IFormModuleRepository : IRepository<FormModule>
    {
        void AddNewModuleInForm(int formId, int moduleId, int position);

        void DeleteModuleFromForm(int formId, int moduleId);

        //
        ICollection<FormModuleGettingOnlyModulesDto> GetAllModulesByFormId(int formId);

        ICollection<FormModelDto> GetModulesByFormId(int formId);
    }
}