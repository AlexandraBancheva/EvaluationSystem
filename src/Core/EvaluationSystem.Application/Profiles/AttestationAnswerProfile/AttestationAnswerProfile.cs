using AutoMapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationAnswerProfile
{
    public class AttestationAnswerProfile : Profile
    {
        public AttestationAnswerProfile()
        {
            CreateMap<AnswerTemplate, AttestationAnswer>();

            CreateMap<CreateFormModuleQuestionAnswerDto, AttestationAnswer>()
                .ForMember(a => a.AnswerText, opts => opts.MapFrom(p => p.AnswerText))
                .ForMember(t => t.Position, opts => opts.MapFrom(k => k.Position));

            CreateMap<AnswerDetailDto, AttestationAnswerDetailDto>();
        }
    }
}
