using AutoMapper;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationModuleProfile
{
    public class AttestationModuleProfile : Profile
    {
        public AttestationModuleProfile()
        {
            CreateMap<ModuleTemplateDto, AttestationModule>();
               // .ForMember(m => m.Name, opts => opts.MapFrom(am => am.Name));
        }
    }
}
