using AutoMapper;
using EvaluationSystem.Application.Models.AttestationQuestions;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationQuestionProfile
{
    public class AttestationQuestionProfile : Profile
    {
        public AttestationQuestionProfile()
        {
            CreateMap<CreateFormModuleQuestionDto, AttestationQuestionDto>()
                .ForMember(q => q.Name, opts => opts.MapFrom(r => r.QuestionName))
                .ForMember(t => t.QuestionPosition, opts => opts.MapFrom(q => q.QuestionPosition))
                .ForMember(y => y.Answers, opts => opts.MapFrom(k => k.Answers));

            CreateMap<AttestationQuestionDto, CreateQuestionDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(t => t.Name));

            CreateMap<CreateQuestionDto, AttestationQuestion>()
                .ForMember(q => q.Name, opts => opts.MapFrom(t => t.QuestionName));

            CreateMap<QuestionDetailDto, AttestationQuestionDetailDto>();
        }
    }
}
