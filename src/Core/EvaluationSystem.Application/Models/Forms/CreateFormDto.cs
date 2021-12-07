﻿using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Models.Forms
{
    public class CreateFormDto
    {
        public CreateFormDto()
        {
            this.Module = new HashSet<CreateFormModuleDto>();
          //  this.Question = new HashSet<CreateFormModuleQuestionDto>();
          // this.Answer = new HashSet<CreateFormModuleQuestionAnswerDto>();
        }

        public string FormName { get; set; }

        public int ModulePosition { get; set; }

        public virtual ICollection<CreateFormModuleDto> Module { get; set; }

        //public int QuestionPosition { get; set; }

        //public ICollection<CreateFormModuleQuestionDto> Question { get; set; }

        //public ICollection<CreateFormModuleQuestionAnswerDto> Answer { get; set; }
    }
}