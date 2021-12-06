using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using System;

namespace EvaluationSystem.Application.Services
{
    public class FormsServices : IFormsServices
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormModuleRepository _formModuleRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleQuestionRepository _moduleQuestionRepository;
        private readonly ICustomQuestionsRepository _customQuestionsRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ICustomQuestionsServices _questionCustomServices;
        private readonly IMapper _mapper;

        public FormsServices(IMapper mapper, 
            IFormRepository formRepository,
            IFormModuleRepository formModuleRepository, 
            IModuleRepository moduleRepository, 
            IModuleQuestionRepository moduleQuestionRepository,
            ICustomQuestionsServices questionCustomServices,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository, 
            ICustomQuestionsRepository customQuestionsRepository)
        {
            _formRepository = formRepository;
            _formModuleRepository = formModuleRepository;
            _moduleRepository = moduleRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
            _customQuestionsRepository = customQuestionsRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _questionCustomServices = questionCustomServices;
            _mapper = mapper;
        }

        // Problem with mapper
        public IEnumerable<FormDetailDto> CreateNewForm(CreateFormDto form)
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
                        var questionId = _questionCustomServices.CreateNewQuestion(moduleId, currentModuleQuestion, _mapper.Map<CreateQuestionDto>(question));
                        answer.IdQuestion = questionId;
                        _answerRepository.Insert(answer);
                    }
                }
            }

           return GetFormById(formId);
        }

        public void DeleteFormById(int formId)
        {
            var formModules = _formModuleRepository.GetAllModulesByFormId(formId);

            foreach (var module in formModules)
            {
                var moduleQuestions = _moduleQuestionRepository.GetAllQuestionsByModuleId(module.IdModule);

                foreach (var question in moduleQuestions)
                {
                    _questionCustomServices.DeleteCustomQuestion(question.IdQuestion);
                    _moduleQuestionRepository.DeleteQuestionFromModule(module.IdModule, question.IdQuestion);
                }

                _formModuleRepository.DeleteModuleFromForm(formId, module.IdModule);
                _moduleRepository.DeleteModule(module.IdModule);
            }
            
            _formRepository.DeleteForm(formId);
        }

        public IEnumerable<FormDetailDto> GetFormById(int formId)
        {
            var res = _formRepository.GetAllWithFormId(formId);
            var results = _mapper.Map<IEnumerable<FormDetailDto>>(res);

            return results;
        }

        public UpdatedFormDto UpdateCurrentForm(int formId, UpdateFormDto form)
        {
            var entity = _mapper.Map<FormTemplate>(form);
            entity.Id = formId;
            _formRepository.Update(entity);

            return _mapper.Map<UpdatedFormDto>(_formRepository.GetById(formId));
        }

        // Don't work!
        //public IEnumerable<ListFormsModulesDto> GetAllFormWithAllInformation()
        //{
        //    var forms = _formRepository.AllForms();
        //    var formsModules = _mapper.Map<IEnumerable<ListFormsModulesDto>>(forms);

        //    return formsModules;
        //}

        //
        public IEnumerable<FormDetailDto> GetAllForsWithAllModules()
        {
            var results = _formRepository.AllForms();

            return _mapper.Map<IEnumerable<FormDetailDto>>(results);
        }
    }
}
