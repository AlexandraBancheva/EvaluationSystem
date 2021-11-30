using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Domain.Enums;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionInModuleDto
    {
        public int Id { get; set; }

        public int QuestionName { get; set; }

        public QuestionType Type { get; set; }

        public DateTime DateOfCreation { get; set; }

        public ICollection<AnswersInQuestionDto> Answers { get; set; }
    }
}
