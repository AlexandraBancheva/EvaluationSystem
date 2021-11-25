using AutoMapper;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Profiles.FormModuleProfile
{
    public class FormModuleProfile : Profile
    {
        public FormModuleProfile()
        {
            CreateMap<FormTemplateDto, ListFormsModulesDto>()
                .ForMember(f => f.FormName, opts => opts.MapFrom(n => n.Name))
                .ForMember(b => b.Postion, opts => opts.MapFrom(n => n.Position));

            CreateMap<ModuleTemplateDto, ListModulesDto>()
                .ForMember(m => m.ModuleName, opts => opts.MapFrom(s => s.Name));
        }
    }
}
