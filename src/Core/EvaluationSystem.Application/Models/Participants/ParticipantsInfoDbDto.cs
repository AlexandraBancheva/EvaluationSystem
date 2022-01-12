using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Models.Participants
{
    public class ParticipantsInfoDbDto
    {
        public string ParticipantUser { get; set; }

        public string ParticipantEmail { get; set; }

       public Status ParticipantStatus { get; set; }
       // public string ParticipantStatus { get; set; }
    }
}