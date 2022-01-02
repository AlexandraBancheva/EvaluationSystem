using System.Collections.Generic;
using EvaluationSystem.Application.Models.AttestationQuestions;

namespace EvaluationSystem.Application.Models
{
    public class AttestationModuleDto
    {
        public AttestationModuleDto()
        {
            this.Questions = new HashSet<AttestationQuestionDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ModulePosition { get; set; }

        public virtual ICollection<AttestationQuestionDto> Questions { get; set; }
    }
}
