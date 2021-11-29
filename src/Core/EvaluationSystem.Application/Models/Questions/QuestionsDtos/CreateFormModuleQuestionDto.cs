using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class CreateFormModuleQuestionDto
    {
        public string QuestionName { get; set; }

        public QuestionType Type { get; set; }

       // public int Position { get; set; }
    }
}
