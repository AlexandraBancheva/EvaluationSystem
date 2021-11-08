using EvaluationSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class UpdateQuestionDto
    {
        public string Name { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }

    }
}
