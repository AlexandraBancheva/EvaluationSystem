using System.Collections.Generic;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Models.Modules.ModulesDtos
{
    public class ModuleInFormDto
    {
        public int Id { get; set; }

        public string ModuleForm { get; set; }

        public int QuestionPosition { get; set; }

        public ICollection<QuestionInModuleDto> Questions { get; set; }
    }
}
