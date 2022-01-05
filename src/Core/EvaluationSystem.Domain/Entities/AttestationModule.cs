using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class AttestationModule
    {
        public AttestationModule()
        {
            this.AttestationQuestions = new HashSet<AttestationQuestion>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AttestationQuestion> AttestationQuestions { get; set; }
    }
}
