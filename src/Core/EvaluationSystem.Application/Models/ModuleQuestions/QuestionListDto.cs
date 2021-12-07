using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.ModuleQuestions
{
    public class QuestionListDto
    {
        public int IdQuestion { get; set; }

        public string QuestionName { get; set; }

        public QuestionType Type { get; set; }

       // public int Position { get; set; }
    }
}
