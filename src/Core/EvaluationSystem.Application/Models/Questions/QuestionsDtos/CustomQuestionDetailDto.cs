using System;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class CustomQuestionDetailDto
    {
        public CustomQuestionDetailDto()
        {
            this.Answers = new HashSet<AnswerDetailDto>();
        }

        public int IdQuestion { get; set; }

        public string QuestionName { get; set; }

        public int Type { get; set; }

        public bool IsReusable { get; set; }

        public int QuestionPosition { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual ICollection<AnswerDetailDto> Answers { get; set; }
    }
}
