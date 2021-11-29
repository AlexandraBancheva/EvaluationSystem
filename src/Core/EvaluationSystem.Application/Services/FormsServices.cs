using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.FormModules;
using System;
using EvaluationSystem.Application.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Services
{
    public class FormsServices : IFormsServices
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormModuleRepository _formModuleRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleQuestionRepository _moduleQuestionRepository;
        private readonly ICustomQuestionsServices _questionCustomServices;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public FormsServices(IMapper mapper, 
            IFormRepository formRepository,
            IFormModuleRepository formModuleRepository, 
            IModuleRepository moduleRepository, 
            IModuleQuestionRepository moduleQuestionRepository,
            ICustomQuestionsServices questionCustomServices,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository)
        {
            _formRepository = formRepository;
            _formModuleRepository = formModuleRepository;
            _moduleRepository = moduleRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
            _questionCustomServices = questionCustomServices;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        // Maybe foreach in foreach????
        // Problem with position
        public FormDetailDto CreateNewForm(CreateFormDto form)
        {
            var currrentForm = _mapper.Map<FormTemplate>(form);

            // var currentFormModule = _mapper.Map<FormModule>(form);  //????
            var currentFormModule = form.ModulePosition;
            var currentModules = _mapper.Map<ICollection<ModuleTemplate>>(form.Module);

            // var currentModuleQuestion = _mapper.Map<ModuleQuestion>(form.Question); // ????

            var currentModuleQuestion = form.QuestionPosition;
            var currentQuestions = _mapper.Map<ICollection<QuestionTemplate>>(form.Question);
            var currentAnswers = _mapper.Map<ICollection<AnswerTemplate>>(form.Answer);

            var formId =_formRepository.Insert(currrentForm);
            foreach (var module in currentModules)
            {
               var moduleId = _moduleRepository.Insert(module);
               _formModuleRepository.AddNewModuleInForm(formId, moduleId, currentFormModule);
                foreach (var question in currentQuestions)
                {
                    foreach (var answer in currentAnswers)
                    {
                        question.IsReusable = false;
                        var questionId = _questionCustomServices.CreateNewQuestion(_mapper.Map<CreateQuestionDto>(question));
                        answer.IdQuestion = questionId;
                        _answerRepository.Insert(answer);
                        _moduleQuestionRepository.AddNewQuestionToModule(moduleId, questionId, currentModuleQuestion);
                    }
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
