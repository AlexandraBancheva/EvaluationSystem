using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Answers.AnswersDtos
{
    public class QuestionByIdWithAnswersListDto
    {
        public string QuestionName { get; set; }

        public List<ListAnswersByQuestionId> Answers { get; set; }
    }
}
