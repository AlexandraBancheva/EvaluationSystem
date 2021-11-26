using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.FormModules;
using System;

namespace EvaluationSystem.Application.Services
{
    public class FormsServices : IFormsServices
    {
        private readonly IFormRepository _formRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public FormsServices(IFormRepository formRepository, IMapper mapper, IModuleRepository moduleRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _formRepository = formRepository;
            _moduleRepository = moduleRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        // Maybe foreach in foreach????
        public FormDetailDto CreateNewForm(CreateFormDto form) // FormDetailDto
        {
            var currrentForm = _mapper.Map<FormTemplate>(form);
            var currentModules = _mapper.Map<ICollection<ModuleTemplate>>(form.Module);
            var currentQuestions = _mapper.Map<ICollection<QuestionTemplate>>(form.Question);
            var currentAnswers = _mapper.Map<ICollection<AnswerTemplate>>(form.Answer);

            var formId =_formRepository.Insert(currrentForm);
            foreach (var module in currentModules) // Check the insert, must give the formId
            {
                _moduleRepository.Insert(module);
            }

            //var listQuestions = new HashSet<int>();
            //foreach (var question in currentQuestions)
            //{
            //   var id = _questionRepository.Insert(question);
            //    listQuestions.Add(id);
            //}

            //foreach (var answer in currentAnswers)
            //{
            //    _answerRepository.Insert(answer);
            //}

            foreach (var question in currentQuestions)
            {
                question.DateOfCreation = DateTime.UtcNow;
                foreach (var answer in currentAnswers)
                {
                    var questionId = _questionRepository.Insert(question);
                    answer.IdQuestion = questionId;
                    _answerRepository.Insert(answer);
                }
            }

           return GetFormById(formId);
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
