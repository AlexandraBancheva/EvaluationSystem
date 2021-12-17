using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionDetailDto
    {
        public QuestionDetailDto()
        {
            this.Answers = new HashSet<AnswerDetailDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool  IsReusable { get; set; }

        public int Position { get; set; }

        public virtual ICollection<AnswerDetailDto> Answers { get; set; }
    }
}
