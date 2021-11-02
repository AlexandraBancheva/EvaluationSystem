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
            new Question(0, "Question1", DateTime.UtcNow, QuestionType.RadioButtons),
            new Question(1, "Question2", DateTime.UtcNow, QuestionType.NumericalOptions),
            new Question(0, "Question3", DateTime.UtcNow, QuestionType.CheckBoxes),
            new Question(0, "Question4", DateTime.UtcNow, QuestionType.TextFiled),
        };

        public ICollection<Question> Questions => questionsList;
    }
}
