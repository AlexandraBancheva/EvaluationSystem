using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.AttestationForms
{
    public class AttestationFormDto
    {
        public AttestationFormDto()
        {
            this.Modules = new HashSet<AttestationModuleDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AttestationModuleDto> Modules { get; set; }
    }
}
