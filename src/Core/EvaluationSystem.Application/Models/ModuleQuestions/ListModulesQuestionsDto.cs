using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.ModuleQuestions
{
    public class ListModulesQuestionsDto
    {
        public ListModulesQuestionsDto()
        {
            this.Questions = new HashSet<QuestionListDto>();
        }

        public int IdModule { get; set; }

        public string ModuleName { get; set; }

        public int Position { get; set; }  // ?

        public virtual ICollection<QuestionListDto> Questions { get; set; }
    }
}