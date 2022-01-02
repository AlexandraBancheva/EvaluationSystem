using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Models.AttestationForms
{
    public class AttestationFormDto
    {
        public AttestationFormDto()
        {
            this.Modules = new HashSet<ModuleTemplateDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ModuleTemplateDto> Modules { get; set; }
    }
}
