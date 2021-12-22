using EvaluationSystem.Domain.Enums;
using System;

namespace EvaluationSystem.Domain.Entities
{
    public class AttestationQuestion
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }
    }
}
