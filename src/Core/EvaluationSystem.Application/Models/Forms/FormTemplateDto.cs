using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

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

        public int Position { get; set; }

        public virtual ICollection<ModuleTemplateDto> Modules { get; set; }
    }
}
