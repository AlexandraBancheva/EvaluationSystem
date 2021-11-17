using AutoMapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
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
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

            CreateMap<AddNewAnswerDto, AnswerTemplate>();
            CreateMap<AnswerTemplate, ListAnswersByQuestionId>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

            CreateMap<UpdateAnswerDto, AnswerTemplate>();

            CreateMap<AnswerTemplate, AnswerListDto>()
                .ForMember(a => a.IdAnswer, opts => opts.MapFrom(t => t.Id))
                .ForMember(y => y.AnswerText, opts => opts.MapFrom(y => y.AnswerText));
        }
    }
}
