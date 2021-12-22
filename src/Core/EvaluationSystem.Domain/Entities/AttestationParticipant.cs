namespace EvaluationSystem.Domain.Entities
{
    public class AttestationParticipant
    {
        public int Id { get; set; }

        public int IdAttestation { get; set; }

        public int IdUserParticipant { get; set; }

        public string Status { get; set; }

        public string Position { get; set; }
    }
}
