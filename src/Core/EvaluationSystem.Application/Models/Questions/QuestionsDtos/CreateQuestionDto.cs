using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Questions.QuestionsDtos
{
    public class CreateQuestionDto
    {
        public string QuestionName { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }
    }
}
