
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Forms
{
    public class UpdateFormDto
    {
        public int Id { get; set; }

        public string FormName { get; set; }

        public int ModulePosition { get; set; }

        public ICollection<UpdateModuleDto> Module { get; set; }

        public int QuestionPosition { get; set; }

        public ICollection<UpdateQuestionDto> Question { get; set; }

        public ICollection<UpdateAnswerDto> Answer { get; set; }
    }
}
