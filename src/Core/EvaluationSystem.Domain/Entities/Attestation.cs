using System;

namespace EvaluationSystem.Domain.Entities
{
    public class Attestation
    {
        public int Id { get; set; }

        public int IdForm { get; set; }

        public int IdUserToEvaluate { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
