namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class ListQuestionsDto
    {
        public int IdQuestion { get; set; }

        public string Name { get; set; }

        public int IdAnswer { get; set; }

        public string AnswerText { get; set; }
    }
}
