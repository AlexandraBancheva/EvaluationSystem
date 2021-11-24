using AutoMapper;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.FormModuleProfile
{
    public class FormModuleProfile : Profile
    {
        public FormModuleProfile()
        {
            CreateMap<FormTemplate, ListFormsModulesDto>()
                .ForMember(f => f.FormName, opts => opts.MapFrom(n => n.Name));

            CreateMap<ModuleTemplate, ListModulesDto>()
                .ForMember(m => m.ModuleName, opts => opts.MapFrom(s => s.Name));
        }
    }
}
