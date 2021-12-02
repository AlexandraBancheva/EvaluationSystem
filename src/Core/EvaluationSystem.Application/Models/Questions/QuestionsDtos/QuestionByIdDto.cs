using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using System.Collections.Generic;

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

        public ICollection<AnswersByQuestionsIdDto> Answers { get; set; }
    }
}
