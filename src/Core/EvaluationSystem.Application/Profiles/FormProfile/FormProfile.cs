using AutoMapper;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Profiles.FormProfile
{
    public class FormProfile :Profile
    {
        public FormProfile()
        {
            CreateMap<CreateFormDto, FormTemplate>()
                .ForMember(f => f.Name, opts => opts.MapFrom(b => b.FormName));

            CreateMap<FormTemplate, FormDetailDto>();
            // .ForMember(f => f.Name, opts => opts.MapFrom(b => b.Name));

            CreateMap<UpdateFormDto, FormTemplate>()
                .ForMember(f => f.Name, opts => opts.MapFrom(b => b.FormName));
        }
    }
}
