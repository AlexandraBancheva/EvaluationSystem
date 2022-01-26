﻿using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Application.Models.AttestationAnswers;

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

        public QuestionType Type { get; set; }

        public bool IsReusable { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string TextAnswer { get; set; }

        public virtual ICollection<AttestationAnswerDetailDto> Answers { get; set; }
    }
}
