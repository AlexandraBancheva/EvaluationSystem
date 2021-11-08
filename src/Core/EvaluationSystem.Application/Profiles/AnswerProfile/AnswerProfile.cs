using AutoMapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AnswerProfile
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AddNewAnswerDto, Answer>();
            CreateMap<Answer, AnswerDetailDto>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

            CreateMap<AddNewAnswerDto, Answer>();
            CreateMap<Answer, ListAnswersByQuestionId>()
                .ForMember(a => a.AnswerName, opts => opts.MapFrom(an => an.AnswerText));

            CreateMap<Answer, QuestionByIdWithAnswersListDto>();
        }
    }
}
