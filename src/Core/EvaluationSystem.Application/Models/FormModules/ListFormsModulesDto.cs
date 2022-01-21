using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.FormModules
{
    public class ListFormsModulesDto
    {
        public ListFormsModulesDto()
        {
            this.Modules = new HashSet<ListModulesDto>();
        }

        public int Id { get; set; }

        public string FormName { get; set; }

        public int Postion { get; set; }

        public virtual ICollection<ListModulesDto> Modules { get; set; }
    }
}
