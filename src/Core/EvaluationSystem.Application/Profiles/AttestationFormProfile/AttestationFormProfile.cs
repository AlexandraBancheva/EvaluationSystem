using AutoMapper;
using EvaluationSystem.Application.Models.AttestationForms;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationFormProfile
{
    public class AttestationFormProfile : Profile
    {
        public AttestationFormProfile()
        {
            // 05.01.22
            CreateMap<CreateFormDto, AttestationFormDto>()
                .ForMember(f => f.Name, opts => opts.MapFrom(p => p.FormName));

            CreateMap<AttestationFormDto, AttestationForm>();
        }
    }
}
