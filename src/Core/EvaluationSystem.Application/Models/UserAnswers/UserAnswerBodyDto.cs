using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.UserAnswers
{
    public class UserAnswerBodyDto
    {
        public UserAnswerBodyDto()
        {
            //this.AnswerIds = new HashSet<int>();
            this.AttestationModuleIds = new HashSet<AttestationModuleIdsDto>();
        }

        public ICollection<AttestationModuleIdsDto> AttestationModuleIds { get; set; }

      //  public int AttestationQuestionId { get; set; }

      //  public string AnswerText { get; set; }

       // public virtual ICollection<int> AnswerIds { get; set; }
    }
}
