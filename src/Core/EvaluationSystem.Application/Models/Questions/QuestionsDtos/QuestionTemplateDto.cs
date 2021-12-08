using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionTemplateDto
    {
        public QuestionTemplateDto()
        {
            this.Answers = new HashSet<AnswerTemplate>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }

        public virtual ICollection<AnswerTemplate> Answers { get; set; }
    }
}
