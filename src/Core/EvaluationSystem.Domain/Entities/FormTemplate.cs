using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class FormTemplate
    {
        public FormTemplate()
        {
            this.Modules = new HashSet<ModuleTemplate>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ModuleTemplate> Modules { get; set; }
    }
}
