using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Models.Forms
{
    public class FormWithAllDto
    {
        public int FormId { get; set; }

        public string FormName { get; set; }

        public int ModulePosition { get; set; }

        public ICollection<ModuleDetailDto> Modules { get; set; }

        //public int QuestionPosition { get; set; }

        //public ICollection<QuestionDetailDto> Questions { get; set; }

        //public ICollection<AnswerDetailDto> Answers { get; set; }
    }
}
