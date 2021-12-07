using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Profiles.ModuleProfile
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<CreateModuleDto, ModuleTemplate>()
                .ForMember(m => m.Name, opts => opts.MapFrom(p => p.ModuleName));

            CreateMap<ModuleTemplate, ModuleDetailDto>()
                .ForMember(m => m.IdModule, opts => opts.MapFrom(v => v.Id))
                .ForMember(m => m.ModuleName, opts => opts.MapFrom(n => n.Name));

            CreateMap<UpdateModuleDto, ModuleTemplate>()
                .ForMember(m => m.Name, opts => opts.MapFrom(p => p.ModuleName));

            //
            CreateMap<CreateFormDto, ModuleTemplate>();

            CreateMap<CreateFormModuleDto, ModuleTemplate>()
                .ForMember(a => a.Name, opts => opts.MapFrom(p => p.ModuleName));

            CreateMap<ModuleTemplate, ModuleDetail>()
                .ForMember(a => a.Name, opts => opts.MapFrom(k => k.Name));

            CreateMap<ModuleInFormDto, ModuleDetailDto>()
                .ForMember(m => m.IdModule, opts => opts.MapFrom(n => n.Id))
                .ForMember(l => l.ModuleName, opts => opts.MapFrom(d => d.Name))
                .ForMember(p => p.Questions, opts => opts.MapFrom(r => r.Questions));

            CreateMap<ModuleTemplate, ModuleDetail>()
                .ForMember(r => r.Name, opts => opts.MapFrom(p => p.Name));

            //
            CreateMap<CreateFormModuleDto, ModuleTemplateDto>()
                .ForMember(a => a.Name, opts => opts.MapFrom(o => o.ModuleName))
                .ForMember(w => w.Questions, opts => opts.MapFrom(t => t.Question))
                .ForMember(y => y.Position, opts => opts.MapFrom(l => l.QuestionPosition));
        }
    }
}
