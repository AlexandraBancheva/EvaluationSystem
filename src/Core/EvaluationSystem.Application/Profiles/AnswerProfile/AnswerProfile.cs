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
            CreateMap<AnswerTemplate, AnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

            CreateMap<AddNewAnswerDto, AnswerTemplate>();
            CreateMap<AnswerTemplate, ListAnswersByQuestionId>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

           // CreateMap<Answer, QuestionByIdWithAnswersListDto>();
        }
    }
}
