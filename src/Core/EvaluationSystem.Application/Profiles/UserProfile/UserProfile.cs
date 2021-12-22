using AutoMapper;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles
{
    public class UserProfile :  Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailDto>();
        }
    }
}
