namespace EvaluationSystem.Application.Models.AttestationAnswers
{
    public class AttestationAnswerDetailDto
    {
        public int IdAnswer { get; set; }

        public string AnswerName { get; set; }

        public int Position { get; set; }

        public bool IsAnswered { get; set; } = false;
    }
}
