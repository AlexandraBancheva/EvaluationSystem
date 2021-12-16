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

            CreateMap<CreateFormDto, ModuleTemplate>();

            CreateMap<CreateFormModuleDto, ModuleTemplate>()
                .ForMember(a => a.Name, opts => opts.MapFrom(p => p.ModuleName));

            CreateMap<ModuleTemplate, ModuleDetail>()
                .ForMember(a => a.Name, opts => opts.MapFrom(k => k.Name));

            CreateMap<ModuleInFormDto, ModuleDetailDto>()
                .ForMember(m => m.IdModule, opts => opts.MapFrom(n => n.IdModule))
                .ForMember(l => l.ModuleName, opts => opts.MapFrom(d => d.Name))
                .ForMember(p => p.Questions, opts => opts.MapFrom(r => r.Questions));

            CreateMap<ModuleTemplate, ModuleDetail>()
                .ForMember(r => r.Name, opts => opts.MapFrom(p => p.Name));

            CreateMap<CreateFormModuleDto, ModuleTemplateDto>()
                .ForMember(a => a.Name, opts => opts.MapFrom(o => o.ModuleName))
                .ForMember(w => w.Questions, opts => opts.MapFrom(t => t.Question));

            CreateMap<ModuleTemplateDto, ModuleTemplate>()
                .ForMember(m => m.Name, opts => opts.MapFrom(y => y.Name))
                .ForMember(n => n.Questions, opts => opts.MapFrom(o => o.Questions));

            CreateMap<ModuleTemplateDto, ModuleDetailDto>()
                .ForMember(m => m.IdModule, opts => opts.MapFrom(p => p.Id))
                .ForMember(o => o.ModuleName, opts => opts.MapFrom(y => y.Name))
                .ForMember(r => r.Position, opts => opts.MapFrom(u => u.ModulePosition));

            CreateMap<ModuleTemplateDto, CurrentModuleDetailDto>()
                .ForMember(m => m.Id, opts => opts.MapFrom(p => p.Id))
                .ForMember(o => o.Name, opts => opts.MapFrom(m => m.Name))
                .ForMember(y => y.Position, opts => opts.MapFrom(r => r.ModulePosition));
        }
    }
}
