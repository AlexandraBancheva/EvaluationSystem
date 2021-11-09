using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class ListQuestionsAnswersDto
    {
        public int IdQuestion { get; set; } //

        public string QuestionName { get; set; }

       // public int IdAnswer { get; set; }  //

        public ICollection<string> Answers { get; set; }
    }
}
