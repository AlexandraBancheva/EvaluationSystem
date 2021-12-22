namespace EvaluationSystem.Domain.Entities
{
    public class AttestationModuleQuestion
    {
        public int IdModuleQuestion { get; set; }

        public int IdAttestationModule { get; set; }

        public int IdAttestationQuestion { get; set; }

        public int Position { get; set; }
    }
}
