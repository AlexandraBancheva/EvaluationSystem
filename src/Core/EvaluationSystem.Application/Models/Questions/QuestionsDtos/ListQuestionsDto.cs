using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class ListQuestionsDto  /// From Repository
    {
        public int IdQuestion { get; set; }

        public string Name { get; set; }

        public int IdAnswer { get; set; }

        public string AnswerText { get; set; }
    }
}
