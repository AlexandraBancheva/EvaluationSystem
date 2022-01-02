using AutoMapper;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationFormProfile
{
    public class AttestationFormProfile : Profile
    {
        public AttestationFormProfile()
        {
            CreateMap<CreateFormDto, AttestationForm>()
                .ForMember(f => f.Name, opts => opts.MapFrom(af => af.FormName));
        }
    }
}
