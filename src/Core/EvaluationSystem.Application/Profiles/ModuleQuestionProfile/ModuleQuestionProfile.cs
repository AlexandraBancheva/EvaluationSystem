using AutoMapper;
using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.ModuleQuestionProfile
{
    public class ModuleQuestionProfile : Profile
    {
        public ModuleQuestionProfile()
        {
            CreateMap<ModuleTemplate, ListModulesQuestionsDto>()
                .ForMember(m => m.IdModule, opts => opts.MapFrom(n => n.Id))
                .ForMember(o => o.ModuleName, opts => opts.MapFrom(p => p.Name))
              //  .ForMember(p => p.Position, opts => opts.MapFrom(t => t.Position))
                .ForMember(w => w.Questions, opts => opts.MapFrom(q => q.Questions));

            //CreateMap<ModuleQuestion, PositionDto>()
            //    .ForMember(p => p.Position, opts => opts.MapFrom(t => t.Position));
        }
    }
}
