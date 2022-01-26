using System.Collections.Generic;
using EvaluationSystem.Application.Models.AttestationModules;

namespace EvaluationSystem.Application.Models.AttestationForms
{
    public class AttestationFormDetailDto
    {
        public AttestationFormDetailDto()
        {
            this.Modules = new HashSet<AttestationModuleDetailDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AttestationModuleDetailDto> Modules { get; set; }
    }
}
