using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.AttestationAnswers;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

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

            CreateMap<AnswerDetailDto, AttestationAnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(b => b.AnswerName));
        }
    }
}
