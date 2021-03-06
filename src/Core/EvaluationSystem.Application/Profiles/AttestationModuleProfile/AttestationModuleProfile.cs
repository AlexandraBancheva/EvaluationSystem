using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.AttestationModules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Profiles.AttestationModuleProfile
{
    public class AttestationModuleProfile : Profile
    {
        public AttestationModuleProfile()
        {
            CreateMap<AttestationModuleDto, AttestationModule>();

            CreateMap<CreateFormModuleDto, AttestationModuleDto>()
                .ForMember(m => m.Name, opts => opts.MapFrom(y => y.ModuleName))
                .ForMember(t => t.ModulePosition, opts => opts.MapFrom(r => r.ModulePosition))
                .ForMember(q => q.Questions, opts => opts.MapFrom(b => b.Question));

            CreateMap<ModuleDetailDto, AttestationModuleDetailDto>();
        }
    }
}
