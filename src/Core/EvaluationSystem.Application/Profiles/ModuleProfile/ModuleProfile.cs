using AutoMapper;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.ModuleProfile
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<CreateModuleDto, ModuleTemplate>()
                .ForMember(m => m.Name, opts => opts.MapFrom(p => p.ModuleName));

            CreateMap<ModuleTemplate, ModuleDetailDto>()
                .ForMember(m => m.ModuleName, opts => opts.MapFrom(n => n.Name));

            CreateMap<UpdateModuleDto, ModuleTemplate>()
                .ForMember(m => m.Name, opts => opts.MapFrom(p => p.ModuleName));
              //  .ForMember(m => m.Id, opts => opts.MapFrom(y => y.IdModule));
        }
    }
}
