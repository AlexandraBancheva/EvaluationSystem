using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class AddNewAnswerDto
    {
        public string AnswerText { get; set; }

        public bool IsDefault { get; set; }

        public int Position { get; set; }
    }
}
