namespace EvaluationSystem.Application.Models.Users
{
    public class UserToEvaluateDto
    {
        public int IdAttestation { get; set; }

        public int IdForm { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
