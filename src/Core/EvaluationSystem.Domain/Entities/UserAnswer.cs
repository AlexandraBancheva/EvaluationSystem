namespace EvaluationSystem.Domain.Entities
{
    public class UserAnswer
    {
        public int Id { get; set; }

        public int IdAttestation { get; set; }

        public int IdUserParticipant { get; set; }

        public int IdAttestationModule { get; set; }

        public int IdAttestationQuestion { get; set; }

        public int? IdAttestationAnswer { get; set; }

        public string TextAnswer { get; set; }
    }
}
