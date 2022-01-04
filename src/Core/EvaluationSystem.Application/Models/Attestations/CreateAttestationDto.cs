using EvaluationSystem.Application.Models.Users;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Attestations
{
    public class CreateAttestationDto
    {
        public CreateAttestationDto()
        {
            this.UserParticipants = new HashSet<UserParticipantDto>();
        }

        public UserInfoDto User { get; set; }

        public int IdForm { get; set; }

        public virtual ICollection<UserParticipantDto> UserParticipants { get; set; }
    }
}
