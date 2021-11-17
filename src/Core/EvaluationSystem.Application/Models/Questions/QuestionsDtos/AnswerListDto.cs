namespace EvaluationSystem.Application.Models.Questions
{
    public class AnswerListDto
    {
        public int IdAnswer { get; set; }

        public string AnswerText { get; set; }

        public bool IsDefault { get; set; }

        public int Position { get; set; }
    }
}
