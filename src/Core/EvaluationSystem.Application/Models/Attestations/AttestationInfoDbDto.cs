using System;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Application.Models.Participants;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class AttestationInfoDbDto
    {
        public AttestationInfoDbDto()
        {
            this.Participants = new HashSet<ParticipantsInfoDbDto>();
        }

        public int IdAttestation { get; set; }

        public  string Username { get; set; }

        public int IdForm { get; set; }

        public string FormName { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<ParticipantsInfoDbDto> Participants { get; set; }
    }
}