namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class UpdateAnswerDto
    {
        public string AnswerText { get; set; }

        public bool? IsDefault { get; set; }

        public int? Position { get; set; }
    }
}