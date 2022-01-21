using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class AddListAnswers
    {
        public AddListAnswers()
        {
            this.Answers = new HashSet<AddNewAnswerDto>();
        }

        public virtual ICollection<AddNewAnswerDto> Answers { get; set; }
    }
}
