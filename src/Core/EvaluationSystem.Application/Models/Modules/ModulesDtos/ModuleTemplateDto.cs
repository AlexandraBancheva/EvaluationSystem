using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Modules.ModulesDtos
{
    public class ModuleTemplateDto
    {
        public ModuleTemplateDto()
        {
            this.Questions = new HashSet<QuestionTemplateDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ModulePosition { get; set; }

        public virtual ICollection<QuestionTemplateDto> Questions { get; set; }
    }
}
