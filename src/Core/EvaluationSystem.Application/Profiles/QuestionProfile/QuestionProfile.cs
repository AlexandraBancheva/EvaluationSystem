using AutoMapper;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

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

            CreateMap<Question, QuestionDetailDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(qn => qn.Name));

            CreateMap<Question, UpdateQuestionDto>();

            CreateMap<UpdateQuestionDto, Question>();

            CreateMap<ListQuestionsDto, ListQuestionsAnswersDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(i => i.Name))
                .ForMember(a => a.IdQuestion, opts => opts.MapFrom(r => r.IdQuestion))
                .ForMember(p => p.Answers, opts => opts.MapFrom(y => y.AnswerText));

            CreateMap<ListQuestionsDto, QuestionNamesDto>();

            //CreateMap<ListQuestionsDto, AnswerListDto1>()
            //    .ForMember(c => c.IdAnswer, opts => opts.MapFrom(w => w.IdAnswer))
            //    .ForMember(p => p.AnswerText, opts => opts.MapFrom(t => t.AnswerText));
        }
    }
}