namespace EvaluationSystem.Domain.Entities
{
    public class Answer
    {
        public Answer()
        {

        }

        public Answer(int id, string answerText, bool isDefault, int position, int questionId)
        {
            this.Id = id;
            this.AnswerText = answerText;
            this.IsDefault = isDefault;
            this.Position = position;
            this.QuestionId = questionId;
        }

        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsDefault { get; set; }

        public int Position { get; set; }

        public int QuestionId { get; set; }
    }
}