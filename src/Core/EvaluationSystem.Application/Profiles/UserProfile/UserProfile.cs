using EvaluationSystem.Application.Models.Users;
using Microsoft.Graph;

namespace EvaluationSystem.Application.Profiles
{
    public class UserProfile :  AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailDto>();

            CreateMap<User, UsersFromCloudDto>()
                .ForMember(a => a.Name, opts => opts.MapFrom(d => d.DisplayName))
                .ForMember(t => t.Email, opts => opts.MapFrom(y => y.UserPrincipalName));

            CreateMap<UsersFromCloudDto, UserDetailDto>();

            CreateMap<Domain.Entities.User, UserDetailDto>()
                .ForMember(u => u.Id, opts => opts.MapFrom(a => a.Id))
                .ForMember(r => r.Name, opts => opts.MapFrom(y => y.Name))
                .ForMember(k => k.Email, opts => opts.MapFrom(g => g.Email));
        }
    }
}
