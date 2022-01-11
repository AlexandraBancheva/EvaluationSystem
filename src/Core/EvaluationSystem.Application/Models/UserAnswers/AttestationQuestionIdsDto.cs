using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.UserAnswers
{
    public class AttestationQuestionIdsDto
    {
        public AttestationQuestionIdsDto()
        {
            this.Answer = new HashSet<AttestationAnswerDto>();
        }

        public int AttestationQuestionId { get; set; }

        public virtual ICollection<AttestationAnswerDto> Answer { get; set; }
    }
}
