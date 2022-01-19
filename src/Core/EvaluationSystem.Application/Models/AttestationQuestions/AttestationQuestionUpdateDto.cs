using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.AttestationQuestions
{
    public class AttestationQuestionUpdateDto
    {
        public AttestationQuestionUpdateDto()
        {
            this.AnswerIds = new HashSet<int>();
        }

        public int AttestationQuestionId { get; set; }

        public string AnswerText { get; set; }

        public virtual ICollection<int> AnswerIds { get; set; }
    }
}
