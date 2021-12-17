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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsReusable { get; set; }

        public int Position { get; set; }

        public virtual ICollection<AnswerDetailDto> Answers { get; set; }
    }
}
