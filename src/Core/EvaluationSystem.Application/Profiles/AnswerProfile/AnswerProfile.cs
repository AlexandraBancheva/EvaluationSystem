using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

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

            CreateMap<UpdateAnswerDto, AnswerTemplate>();

            CreateMap<AnswerTemplate, AnswerListDto>()
                .ForMember(a => a.IdAnswer, opts => opts.MapFrom(t => t.Id))
                .ForMember(y => y.AnswerText, opts => opts.MapFrom(y => y.AnswerText));

            CreateMap<CreateFormDto, AnswerTemplate>();

            CreateMap<AnswerTemplate, AnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(o => o.AnswerText));

            CreateMap<CreateFormModuleQuestionAnswerDto, AnswerTemplate>();

            CreateMap<AnswersInQuestionDto, AnswerDetailDto>()
                .ForMember(a => a.IdAnswer, opts => opts.MapFrom(m => m.IdAnswer))
                .ForMember(v => v.AnswerName, opts => opts.MapFrom(n => n.AnswerText));

            CreateMap<CreateFormModuleQuestionAnswerDto, AnswerTemplate>()
                .ForMember(a => a.AnswerText, opts => opts.MapFrom(p => p.AnswerText));

            CreateMap<AnswersInQuestionDto, CreateFormModuleQuestionAnswerDto>();
        }
    }
}
