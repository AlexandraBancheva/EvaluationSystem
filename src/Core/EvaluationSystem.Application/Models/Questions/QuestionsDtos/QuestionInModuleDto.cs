using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionInModuleDto
    {
        public QuestionInModuleDto()
        {
            this.Answers = new HashSet<AnswersInQuestionDto>();
        }

        public int IdQuestion { get; set; }

        public string Name { get; set; }

        public QuestionType Type { get; set; }

        public DateTime DateOfCreation { get; set; }

        public bool IsReusable { get; set; }

        public int Position { get; set; }

        public virtual ICollection<AnswersInQuestionDto> Answers { get; set; }
    }
}
