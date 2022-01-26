using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;

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
