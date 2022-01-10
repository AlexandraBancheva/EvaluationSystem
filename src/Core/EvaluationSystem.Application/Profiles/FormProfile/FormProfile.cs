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

            CreateMap<UpdateFormDto, FormTemplate>()
                .ForMember(f => f.Name, opts => opts.MapFrom(b => b.FormName));

            CreateMap<CreateFormDto, FormTemplate>()
            .ForMember(f => f.Name, opts => opts.MapFrom(m => m.FormName));

            CreateMap<FormTemplate, FormDetailDto>();

            CreateMap<FormWithAllDto, FormDetailDto>()
                .ForMember(t => t.Id, opts => opts.MapFrom(o => o.Id))
                .ForMember(f => f.Name, opts => opts.MapFrom(v => v.Name))
                .ForMember(b => b.Modules, opts => opts.MapFrom(m => m.Modules));

            CreateMap<FormWithAllDto, FormDetailDto>()
                .ForMember(v => v.Id, opts => opts.MapFrom(a => a.Id))
                .ForMember(b => b.Name, opts => opts.MapFrom(m => m.Name))
                .ForMember(m => m.Modules, opts => opts.MapFrom(i => i.Modules));

            CreateMap<FormTemplate, UpdatedFormDto>()
                .ForMember(f => f.FormName, opts => opts.MapFrom(m => m.Name));

            CreateMap<CreateFormDto, FormTemplateDto>()
                .ForMember(f => f.Name, opts => opts.MapFrom(p => p.FormName))
                .ForMember(t => t.Modules, opts => opts.MapFrom(k => k.Modules));

            CreateMap<FormTemplateDto, FormTemplate>()
                .ForMember(f => f.Name, opts => opts.MapFrom(p => p.Name))
                .ForMember(g => g.Modules, opts => opts.MapFrom(y => y.Modules));

            // AttestationsServices
            //CreateMap<FormDetailDto, CreateFormDto>()
            //    .ForMember(f => f.FormName, opts => opts.MapFrom(d => d.Name))
            //    .ForMember(p => p.Modules, opts => opts.MapFrom(s => s.Modules));
            CreateMap<FormWithAllDto, CreateFormDto>()
                .ForMember(f => f.FormName, opts => opts.MapFrom(y => y.Name));

            CreateMap<FormTemplateDto, FormDetailDto>();
        }
    }
}
