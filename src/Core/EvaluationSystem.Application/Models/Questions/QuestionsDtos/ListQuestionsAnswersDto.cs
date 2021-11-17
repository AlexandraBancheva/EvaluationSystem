using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class ListQuestionsAnswersDto 
    {
        public ListQuestionsAnswersDto()
        {
            this.Answers = new List<AnswerListDto>();
        }

        public int IdQuestion { get; set; }

        public string QuestionName { get; set; }

        public DateTime DateOfCreation { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }

        public ICollection<AnswerListDto> Answers { get; set; }
    }
}
