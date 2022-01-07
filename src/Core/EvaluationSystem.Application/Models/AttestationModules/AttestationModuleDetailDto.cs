using EvaluationSystem.Application.Models.AttestationQuestions;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.AttestationModules
{
    public class AttestationModuleDetailDto
    {
        public AttestationModuleDetailDto()
        {
            this.Questions = new HashSet<AttestationQuestionDetailDto>();
        }

        public int IdModule { get; set; }

        public string ModuleName { get; set; }

        public int Position { get; set; }

        public virtual ICollection<AttestationQuestionDetailDto> Questions { get; set; }
    }
}
