using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.FormModules;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationFormModuleRepository : IRepository<AttestationFormModule>
    {
        void AddModuleInForm(int formId, int moduleId, int position);

        void DeleteModuleFromForm(int formId, int moduleId);

        ICollection<FormModuleGettingOnlyModulesDto> GetAllModulesByFormId(int formId);
    }
}
