using System.Collections.Generic;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Models.Modules.ModulesDtos
{
    public class CreateFormModuleDto
    {
        public CreateFormModuleDto()
        {
            this.Question = new HashSet<CreateFormModuleQuestionDto>();
        }

        public string ModuleName { get; set; }

        public int QuestionPosition { get; set; }

        public virtual ICollection<CreateFormModuleQuestionDto> Question { get; set; }

    }
}