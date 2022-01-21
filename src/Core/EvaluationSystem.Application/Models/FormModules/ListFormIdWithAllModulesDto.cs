using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.FormModules
{
    public class ListFormIdWithAllModulesDto
    {
        public ListFormIdWithAllModulesDto()
        {
            this.Modules = new HashSet<ModuleDetail>();
        }

        public int FormId { get; set; }

        public virtual ICollection<ModuleDetail> Modules { get; set; }
    }
}
