using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Models.AttestationQuestions
{
    public class AttestationQuestionDto
    {
        public AttestationQuestionDto()
        {
            this.Answers = new HashSet<AttestationAnswer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }

        public int QuestionPosition { get; set; }

        public virtual ICollection<AttestationAnswer> Answers { get; set; }
    }
}
