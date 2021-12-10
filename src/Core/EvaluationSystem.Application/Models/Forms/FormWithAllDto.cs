using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Models.Forms
{
    public class FormWithAllDto
    {
        public FormWithAllDto()
        {
            this.Modules = new HashSet<ModuleInFormDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public virtual ICollection<ModuleInFormDto> Modules { get; set; }
    }
}