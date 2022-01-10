using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Models.Forms
{
    public class FormTemplateDto
    {
        public FormTemplateDto()
        {
            this.Modules = new HashSet<ModuleTemplateDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ModuleTemplateDto> Modules { get; set; }
    }
}
