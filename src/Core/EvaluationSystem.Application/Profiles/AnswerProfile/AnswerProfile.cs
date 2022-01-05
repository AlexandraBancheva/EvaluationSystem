using AutoMapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AnswerProfile
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AddNewAnswerDto, AnswerTemplate>();

            CreateMap<AnswerTemplate, AnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText))
                .ForMember(a => a.IdAnswer, opts => opts.MapFrom(b => b.Id));

            CreateMap<AddNewAnswerDto, AnswerTemplate>();

            CreateMap<UpdateAnswerDto, AnswerTemplate>();

            CreateMap<AnswerTemplate, AnswerListDto>()
                .ForMember(a => a.IdAnswer, opts => opts.MapFrom(t => t.Id))
                .ForMember(y => y.AnswerText, opts => opts.MapFrom(y => y.AnswerText));

            CreateMap<CreateFormDto, AnswerTemplate>();
            CreateMap<AnswerTemplate, AnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(o => o.AnswerText));

            CreateMap<CreateFormModuleQuestionAnswerDto, AnswerTemplate>();

            CreateMap<AnswerTemplate, AnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(b => b.AnswerText));

            //
            CreateMap<AnswersInQuestionDto, AnswerDetailDto>()
                .ForMember(a => a.IdAnswer, opts => opts.MapFrom(m => m.IdAnswer))
                .ForMember(v => v.AnswerName, opts => opts.MapFrom(n => n.AnswerText));

            //
            CreateMap<CreateFormModuleQuestionAnswerDto, AnswerTemplate>()
                .ForMember(a => a.AnswerText, opts => opts.MapFrom(p => p.AnswerText));

            //
            CreateMap<AnswerTemplate, AnswerListDto>()
                .ForMember(a => a.IdAnswer, opts => opts.MapFrom(p => p.Id))
                .ForMember(r => r.AnswerText, opts => opts.MapFrom(k => k.AnswerText));

            // AttestationsServices
            //CreateMap<AnswerDetailDto, CreateFormModuleQuestionAnswerDto>()
            //    .ForMember(a => a.AnswerText, opts => opts.MapFrom(r => r.AnswerName));
            CreateMap<AnswersInQuestionDto, CreateFormModuleQuestionAnswerDto>();
        }
    }
}
