namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class AnswersByQuestionsIdDto
    {
        public int Id { get; set; }

        public string AnswerText { get; set; }

        public int Position { get; set; }

        public bool IsReusable { get; set; }
    }
}
