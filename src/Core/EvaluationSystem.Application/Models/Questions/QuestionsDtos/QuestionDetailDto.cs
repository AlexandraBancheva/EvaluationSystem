﻿using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionDetailDto
    {
        //public QuestionDetailDto()
        //{
        //    this.Answers = new HashSet<AnswerDetailDto>();
        //}

        public int Id { get; set; }

        public string QuestionName { get; set; }

        public string Type { get; set; }

        public bool  IsReusable { get; set; }

       // public virtual ICollection<AnswerDetailDto> Answers { get; set; }

    }
}
