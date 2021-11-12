namespace EvaluationSystem.Domain.Entities
{
    public class AnswerTemplate
    {

        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsDefault { get; set; }

        public int Position { get; set; }

        public int QuestionId { get; set; }
    }
}