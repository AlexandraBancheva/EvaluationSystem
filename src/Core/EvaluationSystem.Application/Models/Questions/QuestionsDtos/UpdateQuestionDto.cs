using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class UpdateQuestionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public QuestionType? Type { get; set; }

      //  public bool? IsReusable { get; set; }

    }
}
