﻿using EvaluationSystem.Domain.Enums;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class Question
    {
      
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }

       // public virtual ICollection<Answer> Answers { get; set; } 
        public string AnswerText { get; set; }
    }
}
