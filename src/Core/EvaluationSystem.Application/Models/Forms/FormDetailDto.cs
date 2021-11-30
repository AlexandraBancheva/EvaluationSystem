using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Forms
{
    public class FormDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ModuleDetailDto> Modules { get; set; }

        public virtual ICollection<QuestionDetailDto> Questions { get; set; }

        public virtual ICollection<AnswerDetailDto> Answers { get; set; }
    }
}
