namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class CreateFormModuleQuestionAnswerDto
    {
        public string AnswerText { get; set; }

        public bool IsDefault { get; set; }

        public int Position { get; set; }
    }
}
