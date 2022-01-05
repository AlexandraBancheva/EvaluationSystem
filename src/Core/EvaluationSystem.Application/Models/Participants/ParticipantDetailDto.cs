using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Participants
{
    public class ParticipantDetailDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string ParticipantStatus { get; set; }
    }
}
