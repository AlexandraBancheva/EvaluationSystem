using AutoMapper;
using EvaluationSystem.Application.Models;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationModuleProfile
{
    public class AttestationModuleProfile : Profile
    {
        public AttestationModuleProfile()
        {
            CreateMap<AttestationModuleDto, AttestationModule>();

            // 05.01.22
            CreateMap<CreateFormModuleDto, AttestationModuleDto>()
                .ForMember(m => m.Name, opts => opts.MapFrom(y => y.ModuleName))
                .ForMember(t => t.ModulePosition, opts => opts.MapFrom(r => r.ModulePosition))
                .ForMember(q => q.Questions, opts => opts.MapFrom(b => b.Question));
        }
    }
}
