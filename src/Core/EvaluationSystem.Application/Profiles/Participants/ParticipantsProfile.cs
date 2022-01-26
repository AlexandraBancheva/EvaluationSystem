using AutoMapper;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Models.Participants;

namespace EvaluationSystem.Application.Profiles.Participants
{
    public class ParticipantsProfile : Profile
    {
        public ParticipantsProfile()
        {
            CreateMap<UserParticipantDto, CreateUserParticipantsDto>();

            CreateMap<CreateUserParticipantsDto, ParticipantDetailDto>()
                .ForMember(p => p.ParticipantStatus, opts => opts.MapFrom(pa => pa.Position));
        }
    }
}
