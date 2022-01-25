using AutoMapper;
using EvaluationSystem.Application.Models.AttestationForms;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.UserAnswers;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.AttestationFormProfile
{
    public class AttestationFormProfile : Profile
    {
        public AttestationFormProfile()
        {
            CreateMap<CreateFormDto, AttestationFormDto>()
                .ForMember(f => f.Name, opts => opts.MapFrom(p => p.FormName));

            CreateMap<AttestationFormDto, AttestationForm>();

            CreateMap<FormDetailDto, AttestationFormDetailDto>()
                .ForMember(f => f.Modules, opts => opts.MapFrom(p => p.Modules))
                .ForMember(g => g.Name, opts => opts.MapFrom(r => r.Name))
                .ForMember(y => y.Id, opts => opts.MapFrom(k => k.Id));
        }
    }
}
