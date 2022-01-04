using EvaluationSystem.Application.Models.Participants;
using EvaluationSystem.Domain.Enums;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class AttestationInfoDbDto
    {
        public AttestationInfoDbDto()
        {
            this.Participants = new HashSet<ParticipantsInfoDbDto>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string FormName { get; set; }

        public Status Status { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual ICollection<ParticipantsInfoDbDto> Participants { get; set; }
    }
}
