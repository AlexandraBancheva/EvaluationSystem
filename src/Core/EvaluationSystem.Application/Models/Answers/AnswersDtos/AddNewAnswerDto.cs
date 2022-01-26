using System.ComponentModel;

namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class AddNewAnswerDto
    {
        public string AnswerText { get; set; }

        [DefaultValue("false")]
        public bool IsDefault { get; set; }

        public int Position { get; set; }
    }
}
