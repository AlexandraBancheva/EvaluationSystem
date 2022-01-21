using AutoMapper;
using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Profiles.ModuleQuestionProfile
{
    public class ModuleQuestionProfile : Profile
    {
        public ModuleQuestionProfile()
        {
            CreateMap<ModuleTemplateDto, ListModulesQuestionsDto>()
                .ForMember(m => m.IdModule, opts => opts.MapFrom(n => n.Id))
                .ForMember(o => o.ModuleName, opts => opts.MapFrom(p => p.Name))
                .ForMember(mt => mt.Position, opts => opts.MapFrom(l => l.ModulePosition))
                .ForMember(w => w.Questions, opts => opts.MapFrom(q => q.Questions));

            CreateMap<QuestionTemplateDto, QuestionListDto>()
                .ForMember(p => p.IdQuestion, opts => opts.MapFrom(o => o.Id))
                .ForMember(y => y.QuestionName, opts => opts.MapFrom(v => v.Name));
        }
    }
}
