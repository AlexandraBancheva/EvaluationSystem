using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class CustomQuestionDetailDto
    {
        public CustomQuestionDetailDto()
        {
            this.Answers = new HashSet<AnswerDetailDto>();
        }

        public int IdQuestion { get; set; }

        //
        public string QuestionName { get; set; }

        public int Type { get; set; }

        public bool IsReusable { get; set; }

        public int QuestionPosition { get; set; }

        public virtual ICollection<AnswerDetailDto> Answers { get; set; }
    }
}
