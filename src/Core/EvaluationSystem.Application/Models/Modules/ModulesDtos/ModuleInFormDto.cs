using System.Collections.Generic;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Models.Modules.ModulesDtos
{
    public class ModuleInFormDto
    {
        public ModuleInFormDto()
        {
            this.Questions = new HashSet<QuestionInModuleDto>();
        }

        public int IdModule { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public virtual ICollection<QuestionInModuleDto> Questions { get; set; }
    }
}
