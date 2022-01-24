using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Profiles.QuestionProfile
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<CreateQuestionDto, QuestionTemplate>()
                .ForMember(q => q.Name, opts => opts.MapFrom(qd => qd.QuestionName))
                .ForMember(q => q.Type, opts => opts.MapFrom(t => t.Type)).ReverseMap();

            CreateMap<QuestionTemplate, ListQuestionsDto>()
                .ForMember(q => q.Name, opts => opts.MapFrom(qn => qn.Name))
                .ForMember(q => q.IdQuestion, opts => opts.MapFrom(u => u.Id));

            CreateMap<QuestionTemplate, QuestionDetailDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(qn => qn.Name))
                .ForMember(p => p.IdQuestion, opts => opts.MapFrom(k => k.Id))
                .ForMember(q => q.Type, opts => opts.MapFrom(a => a.Type))
                .ForMember(p => p.DateOfCreation, opts => opts.MapFrom(k => k.DateOfCreation));

            CreateMap<QuestionTemplate, ListQuestionsAnswersDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(y => y.Name))
                .ForMember(f => f.IdQuestion, opts => opts.MapFrom(v => v.Id))
                .ForMember(r => r.Answers, opts => opts.MapFrom(u => u.Answers));

            CreateMap<QuestionTemplate, UpdateQuestionDto>()
                .ForAllMembers(opts => opts.Condition((src, destination, srcMembers) => srcMembers != null));

            CreateMap<UpdateQuestionDto, QuestionTemplate>()
                .ForAllMembers(opts => opts.Condition((src, destination, srcMembers) => srcMembers != null));

            CreateMap<QuestionTemplate, QuestionListDto>()
                .ForMember(v => v.IdQuestion, opts => opts.MapFrom(b => b.Id))
                .ForMember(r => r.QuestionName, opts => opts.MapFrom(c => c.Name));

            CreateMap<CreateFormDto, QuestionTemplate>();

            CreateMap<CreateFormModuleQuestionDto, QuestionTemplate>()
                .ForMember(q => q.Name, opts => opts.MapFrom(a => a.QuestionName));

            CreateMap<QuestionInModuleDto, QuestionDetailDto>()
                .ForMember(w => w.IdQuestion, opts => opts.MapFrom(u => u.IdQuestion))
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(r => r.Name))
                .ForMember(p => p.Answers, opts => opts.MapFrom(a => a.Answers));


            CreateMap<CreateFormModuleQuestionDto, QuestionTemplateDto>()
                .ForMember(t => t.Name, opts => opts.MapFrom(y => y.QuestionName))
                .ForMember(i => i.Answers, opts => opts.MapFrom(t => t.Answers));

            CreateMap<QuestionTemplateDto, QuestionTemplate>()
                .ForMember(q => q.Name, opts => opts.MapFrom(y => y.Name))
                .ForMember(b => b.Answers, opts => opts.MapFrom(t => t.Answers));

            CreateMap<QuestionTemplateDto, CreateQuestionDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(t => t.Name));

            CreateMap<QuestionTemplateDto, QuestionListDto>()
                .ForMember(a => a.IdQuestion, opts => opts.MapFrom(t => t.Id))
                .ForMember(o => o.QuestionName, opts => opts.MapFrom(l => l.Name))
                .ForMember(k => k.QuestionPosition, opts => opts.MapFrom(y => y.QuestionPosition));

            CreateMap<QuestionTemplateDto, QuestionDetailDto>()
                .ForMember(q => q.IdQuestion, opts => opts.MapFrom(p => p.Id))
                .ForMember(a => a.QuestionName, opts => opts.MapFrom(k => k.Name))
                .ForMember(p => p.DateOfCreation, opts => opts.MapFrom(d => d.DateOfCreation));

            CreateMap<QuestionTemplateDto, CustomQuestionDetailDto>()
                .ForMember(q => q.IdQuestion, opts => opts.MapFrom(t => t.Id))
                .ForMember(p => p.QuestionPosition, opts => opts.MapFrom(h => h.QuestionPosition))
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(l => l.Name))
                .ForMember(p => p.DateOfCreation, opts => opts.MapFrom(j => j.DateOfCreation));

            CreateMap<QuestionInModuleDto, CreateFormModuleQuestionDto>()
                .ForMember(q => q.QuestionName, opts => opts.MapFrom(t => t.Name))
                .ForMember(p => p.QuestionPosition, opts => opts.MapFrom(o => o.Position));

            CreateMap<QuestionTemplate, CustomQuestionDetailDto>()
                .ForMember(a => a.QuestionName, opts => opts.MapFrom(r => r.Name));
        }
    }
}