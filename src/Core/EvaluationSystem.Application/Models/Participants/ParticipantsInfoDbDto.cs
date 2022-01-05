using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Participants
{
    public class ParticipantsInfoDbDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Status ParticipantStatus { get; set; }
    }
}
