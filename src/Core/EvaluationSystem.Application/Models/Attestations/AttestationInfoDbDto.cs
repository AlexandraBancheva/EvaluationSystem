using EvaluationSystem.Application.Models.Participants;
using EvaluationSystem.Application.Models.Users;
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

        public UserInfoDto User { get; set; }

        public string FormName { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<ParticipantsInfoDbDto> Participants { get; set; }
    }
}
