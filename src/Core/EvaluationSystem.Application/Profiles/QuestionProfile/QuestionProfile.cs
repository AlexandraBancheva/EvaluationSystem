using AutoMapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.QuestionProfile
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<CreateQuestionDto, Question>()
                .ForMember(q => q.Name, opts => opts.MapFrom(qd => qd.QuestionName))
                .ForMember(q => q.Type, opts => opts.MapFrom(t => t.Type));

            CreateMap<Question, ListQuestionsDto>()
                .ForMember(q => q.Name, opts => opts.MapFrom(qn => qn.Name))
                .ForMember(q => q.IdQuestion, opts => opts.MapFrom(u => u.Id));

            //CreateMap<Answer, ListQuestionsDto>()
            //    .ForMember(a => a.IdAnswer, opts => opts.MapFrom(p => p.Id));

            CreateMap<Question, QuestionDetailDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(qn => qn.Name));

            CreateMap<Question, UpdateQuestionDto>();

            CreateMap<UpdateQuestionDto, Question>();

            //CreateMap<ListQuestionsDto, ListQuestionsAnswersDto>()
            //    .ForMember(q => q.QuestionName, opts => opts.MapFrom(i => i.Name));
        }
    }
}