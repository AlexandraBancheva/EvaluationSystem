using EvaluationSystem.Application.Models.AttestationAnswers;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.AttestationQuestions
{
    public class AttestationQuestionDetailDto
    {
        public AttestationQuestionDetailDto()
        {
            this.Answers = new HashSet<AttestationAnswerDetailDto>();
        }

        public int IdQuestion { get; set; }

        public string QuestionName { get; set; }

        public int Type { get; set; }

        public bool IsReusable { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual ICollection<AttestationAnswerDetailDto> Answers { get; set; }

        public string TextAnswer { get; set; } = null;
    }
}
