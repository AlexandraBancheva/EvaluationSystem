using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Models.Forms
{
    public class CreateFormDto
    {
        public CreateFormDto()
        {
            this.Modules = new HashSet<CreateFormModuleDto>();
        }

        public string FormName { get; set; }

       // public int ModulePosition { get; set; }

        public virtual ICollection<CreateFormModuleDto> Modules { get; set; }
    }
}