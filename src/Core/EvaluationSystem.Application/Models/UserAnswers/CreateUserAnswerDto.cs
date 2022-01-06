using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.UserAnswers
{
    public class CreateUserAnswerDto
    {
        public CreateUserAnswerDto()
        {
            this.Body = new HashSet<UserAnswerBodyDto>();
        }

        public int IdAttestation { get; set; }

        public virtual ICollection<UserAnswerBodyDto> Body { get; set; }
    }
}
