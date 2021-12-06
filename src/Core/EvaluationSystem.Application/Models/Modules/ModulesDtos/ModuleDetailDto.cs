using System.Collections.Generic;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Models.Modules
{
    public class ModuleDetailDto
    {
        public ModuleDetailDto()
        {
            this.Questions = new HashSet<QuestionDetailDto>();
        }

        public int IdModule { get; set; }

        public string ModuleName { get; set; }

        public int Position { get; set; }

        public virtual ICollection<QuestionDetailDto> Questions { get; set; }
    }
}