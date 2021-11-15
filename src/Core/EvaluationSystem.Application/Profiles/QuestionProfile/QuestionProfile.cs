using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Profiles.QuestionProfile
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<CreateQuestionDto, QuestionTemplate>()
                .ForMember(q => q.Name, opts => opts.MapFrom(qd => qd.QuestionName))
                .ForMember(q => q.Type, opts => opts.MapFrom(t => t.Type));

            CreateMap<QuestionTemplate, ListQuestionsDto>()
                .ForMember(q => q.Name, opts => opts.MapFrom(qn => qn.Name))
                .ForMember(q => q.IdQuestion, opts => opts.MapFrom(u => u.Id));

            CreateMap<QuestionTemplate, QuestionDetailDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(qn => qn.Name));

            CreateMap<QuestionTemplate, UpdateQuestionDto>()
                .ForAllMembers(opts => opts.Condition((src, destination, srcMembers) => srcMembers != null));    // Do not work

            CreateMap<UpdateQuestionDto, QuestionTemplate>()
                .ForAllMembers(opts => opts.Condition((src, destination, srcMembers) => srcMembers != null)); ;  // Do not work

            //CreateMap<ListQuestionsDto, ListQuestionsAnswersDto>()
            //    .ForMember(q => q.QuestionName, opts => opts.MapFrom(i => i.Name))
            //    .ForMember(a => a.IdQuestion, opts => opts.MapFrom(r => r.IdQuestion))
            //    .ForMember(p => p.Answers, opts => opts.MapFrom(y => y.AnswerText));

            CreateMap<ListQuestionsDto, QuestionNamesDto>();

            CreateMap<QuestionTemplate, ListQuestionsAnswersDto>();

            CreateMap<AnswerTemplate, AnswerListDto1>();
                //.ForMember(q => q.IdQuestion, opts => opts.MapFrom(r => r.Id))
                //.ForMember(w => w.QuestionName, opts => opts.MapFrom(t => t.Id))
                //.ForMember(y => y.IdAnswer, opts => opts.MapFrom(l => l.Answers))
                //.ForMember(m => m.AnswerText, opts => opts.MapFrom(g => g.Answers));

            //CreateMap<ListQuestionsDto, AnswerListDto1>()
            //    .ForMember(c => c.IdAnswer, opts => opts.MapFrom(w => w.IdAnswer))
            //    .ForMember(p => p.AnswerText, opts => opts.MapFrom(t => t.AnswerText));
        }
    }
}