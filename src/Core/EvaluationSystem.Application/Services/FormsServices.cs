using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Services
{
    public class FormsServices : IFormsServices
    {
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;

        public FormsServices(IFormRepository formRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
        }

        public FormDetailDto CreateNewForm(CreateFormDto form)
        {
            var currentEntity = _mapper.Map<FormTemplate>(form);
            var newEntityId =_formRepository.Insert(currentEntity);

            return GetFormById(newEntityId);
        }

        public void DeleteFormById(int formId)
        {
            _formRepository.DeleteForm(formId);
        }

        public FormDetailDto GetFormById(int formId)
        {
           var res = _formRepository.GetById(formId);
           return _mapper.Map<FormDetailDto>(res);
        }

        public FormDetailDto UpdateCurrentForm(int formId, UpdateFormDto form)
        {
            var entity = _mapper.Map<FormTemplate>(form);
            entity.Id = formId;
            _formRepository.Update(entity);

            return GetFormById(formId);
        }

        public IEnumerable<ListFormsModulesDto> GetAllForsWithAllModules()
        {
            var forms = _formRepository.FormsWithModules();

            var formsModules = _mapper.Map<IEnumerable<ListFormsModulesDto>>(forms);

            return formsModules;
        }
    }
}
