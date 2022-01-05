using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class AttestationForm
    {
        public AttestationForm()
        {
            this.AttestationModules = new HashSet<AttestationModule>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AttestationModule> AttestationModules { get; set; }
    }
}
