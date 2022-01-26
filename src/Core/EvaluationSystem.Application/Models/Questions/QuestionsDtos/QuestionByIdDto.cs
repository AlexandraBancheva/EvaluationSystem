using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionByIdDto
    {
        public QuestionByIdDto()
        {
            this.Answers = new HashSet<AnswersByQuestionsIdDto>();
        }

        public int Id { get; set; }

        public string QuestionName { get; set; }

        public string Type { get; set; }

        public bool IsReusable { get; set; }

        public virtual ICollection<AnswersByQuestionsIdDto> Answers { get; set; }
    }
}
