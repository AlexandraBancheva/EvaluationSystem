using EvaluationSystem.Application.Models.Participants;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class AttestationDetailDto
    {
        public AttestationDetailDto()
        {
            this.UserParticipants = new HashSet<ParticipantDetailDto>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string FormName { get; set; }

        public string Status { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual ICollection<ParticipantDetailDto> UserParticipants { get; set; }
    }
}
