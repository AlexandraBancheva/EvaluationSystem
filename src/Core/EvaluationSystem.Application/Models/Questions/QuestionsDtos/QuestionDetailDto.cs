using System;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionDetailDto
    {
        public QuestionDetailDto()
        {
            this.Answers = new HashSet<AnswerDetailDto>();
        }

        public int IdQuestion { get; set; }

        public string QuestionName { get; set; }

        public QuestionType Type { get; set; }

        public bool  IsReusable { get; set; }

        public DateTime DateOfCreation { get; set; }

        // 24.01.22
        public string TextAnswer { get; set; }

        public virtual ICollection<AnswerDetailDto> Answers { get; set; }
    }
}
