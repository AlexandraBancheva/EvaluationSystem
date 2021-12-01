using System;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Models.Forms
{
    public class FormWithAllDto
    {
        public int FormId { get; set; }

        public string FormName { get; set; }

        public int ModulePosition { get; set; }

        //public int IdModule { get; set; }

        //public string ModuleForm { get; set; }


        //public int IdQuestion { get; set; }

        //public int QuestionName { get; set; }

        //public QuestionType Type { get; set; }

        //public DateTime DateOfCreation { get; set; }

        //public int IdAnswer { get; set; }

        //public string AnswerText { get; set; }

        //public int Position { get; set; }

        public ICollection<ModuleInFormDto> Modules { get; set; }

        public int QuestionPosition { get; set; }

        public ICollection<QuestionDetailDto> Questions { get; set; }

        public ICollection<AnswerDetailDto> Answers { get; set; }
    }
}