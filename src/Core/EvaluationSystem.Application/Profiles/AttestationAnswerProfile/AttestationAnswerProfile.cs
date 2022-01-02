using AutoMapper;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationAnswerProfile
{
    public class AttestationAnswerProfile : Profile
    {
        public AttestationAnswerProfile()
        {
            CreateMap<AnswerTemplate, AttestationAnswer>();
        }
    }
}
