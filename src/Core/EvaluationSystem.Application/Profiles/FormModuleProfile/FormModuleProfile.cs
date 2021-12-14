using AutoMapper;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.FormModuleProfile
{
    public class FormModuleProfile : Profile
    {
        public FormModuleProfile()
        {
            CreateMap<FormTemplateDto, ListFormsModulesDto>()
                .ForMember(f => f.FormName, opts => opts.MapFrom(n => n.Name));

            CreateMap<ModuleTemplateDto, ListModulesDto>()
                .ForMember(m => m.ModuleName, opts => opts.MapFrom(s => s.Name));

            CreateMap<CreateFormModuleDto, FormModule>()
               .ForMember(y => y.Position, opts => opts.MapFrom(p => p.ModulePosition));

            //CreateMap<ModuleTemplate, ListFormIdWithAllModulesDto>()
            //    .ForMember(m => m.FormId, )

            CreateMap<FormModelDto, ListFormIdWithAllModulesDto>()
                .ForMember(f => f.FormId, opts => opts.MapFrom(m => m.IdForm));
        }
    }
}