using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Domain.Enums;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class FakeDatabase
    {
        private readonly ICollection<Question> questionsList = new List<Question>
        {
          //  new Question(0, "Question1", DateTime.UtcNow, QuestionType.RadioButtons),
         //   new Question(1, "Question2", DateTime.UtcNow, QuestionType.NumericalOptions),
         //   new Question(3, "Question3", DateTime.UtcNow, QuestionType.CheckBoxes),
         //   new Question(3, "Question4", DateTime.UtcNow, QuestionType.TextFiled),
        };

        private readonly ICollection<Answer> answersList = new List<Answer>
        { 
            //new Answer(0, "Answer1", 0, "Question1"),
            //new Answer(1, "Answer2", 0, "Question1"),
            //new Answer(2, "Answer3", 0, "Question1"),
            //new Answer(3, "Answer4", 0, "Question1"),
            //new Answer(4, "AnswerZero", 1, "Question2"),
            //new Answer(5, "AnswerZeroOne", 1, "Question2"),
            //new Answer(6, "AnswerZeroTwo", 1, "Question2"),
            //new Answer(7, "AnswerZeroThree", 1, "Question2"),
            //new Answer(8, "AnswerZeroFour", 1, "Question2"),
        };

        public ICollection<Question> Questions => questionsList;

        public ICollection<Answer> Answers => answersList;
    }
}
