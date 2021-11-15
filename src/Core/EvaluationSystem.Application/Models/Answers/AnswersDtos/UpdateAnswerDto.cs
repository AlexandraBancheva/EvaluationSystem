using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class UpdateAnswerDto
    {
        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool? IsDefault { get; set; }

        public int? Position { get; set; }
    }
}
