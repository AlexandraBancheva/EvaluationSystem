using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class ListQuestionsAnswersDto 
    {
        public ListQuestionsAnswersDto()
        {
            this.Answers = new List<AnswerListDto1>();
        }

        public int IdQuestion { get; set; } //

        public string QuestionName { get; set; }

       // public int IdAnswer { get; set; }  //

       // public string AnswerText { get; set; }

        public ICollection<AnswerListDto1> Answers { get; set; }
    }
}
