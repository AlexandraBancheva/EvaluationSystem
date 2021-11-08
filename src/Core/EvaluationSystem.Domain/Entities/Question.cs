using EvaluationSystem.Domain.Enums;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class Question
    {
        public Question()
        {

        }

        public Question(int id, string name, DateTime dateOfCreation, QuestionType type, bool isReusable)
        {
            this.Id = id;
            this.Name = name;
            this.DateOfCreation = dateOfCreation;
            this.Type = type;
            this.IsReusable = isReusable;
            this.Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
