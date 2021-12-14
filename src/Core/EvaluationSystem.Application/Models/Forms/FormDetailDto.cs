using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Models.Forms
{
    public class FormDetailDto
    {
        public FormDetailDto()
        {
            this.Modules = new HashSet<ModuleDetailDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public virtual ICollection<ModuleDetailDto> Modules { get; set; }
    }
}