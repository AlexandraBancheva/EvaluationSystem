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

        public Question(int id, string name, DateTime dateOfCreation, QuestionType type)
        {
            this.Id = id;
            this.Name = name;
            this.DateOfCreation = dateOfCreation;
            this.Type = type;
            this.Answers = new HashSet<Answer>();
        }

        //public Question()
        //{
        //    this.Answers = new HashSet<Answer>();
        //}

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public QuestionType Type { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
