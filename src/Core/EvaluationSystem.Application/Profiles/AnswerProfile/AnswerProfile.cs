using AutoMapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AnswerProfile
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AddNewAnswerDto, AnswerTemplate>();
                //.ForMember(a => a.QuestionId, opts => opts.MapFrom(b => b.));


            CreateMap<AnswerTemplate, AnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

            CreateMap<AddNewAnswerDto, AnswerTemplate>();
            CreateMap<AnswerTemplate, ListAnswersByQuestionId>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

            CreateMap<UpdateAnswerDto, AnswerTemplate>();
        }
    }
}
