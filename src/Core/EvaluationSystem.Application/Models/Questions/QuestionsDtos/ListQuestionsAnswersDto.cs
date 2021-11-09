using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class ListQuestionsAnswersDto
    {
        public string QuestionName { get; set; }

        public ICollection<string> Answers { get; set; }
    }
}
