using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.UserAnswers
{
    public class AttestationModuleIdsDto
    {
        public AttestationModuleIdsDto()
        {
            this.QuestionIds = new HashSet<AttestationQuestionIdsDto>();
        }

        public int AttestationModuleId { get; set; }

        public virtual ICollection<AttestationQuestionIdsDto> QuestionIds { get; set; }
    }
}
