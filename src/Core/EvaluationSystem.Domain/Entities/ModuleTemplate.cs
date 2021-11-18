using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class ModuleTemplate
    {
        public ModuleTemplate()
        {
            this.Questions = new HashSet<QuestionTemplate>();
        }

        public int IdModule { get; set; }

        public string Name { get; set; }

        public ICollection<QuestionTemplate> Questions { get; set; }
    }
}
