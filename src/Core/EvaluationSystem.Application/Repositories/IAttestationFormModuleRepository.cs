using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationFormModuleRepository : IRepository<AttestationFormModule>
    {
        void AddModuleInForm(int formId, int moduleId, int position);

        ICollection<FormModuleGettingOnlyModulesDto> GetAllModulesByFormId(int formId);

        void DeleteModuleFromForm(int formId, int moduleId);
    }
}
