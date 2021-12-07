﻿using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Questions.QuestionsDtos;

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

        
        public IEnumerable<FormDetailDto> CreateNewForm(CreateFormDto form)
        {
            var currentForm = _mapper.Map<FormTemplateDto>(form);
            var formId = _formRepository.Insert(_mapper.Map<FormTemplate>(currentForm));

            foreach (var module in currentForm.Modules)
            {
                var moduleId = _moduleRepository.Insert(_mapper.Map<ModuleTemplate>(module));
                _formModuleRepository.AddNewModuleInForm(formId, moduleId, currentForm.Position);

                foreach (var question in module.Questions)
                {
                    foreach (var answer in question.Answers)
                    {
                        question.IsReusable = false;
                        var questionId = _questionCustomServices.CreateNewQuestion(moduleId, module.Position, _mapper.Map<CreateQuestionDto>(question));
                        answer.IdQuestion = questionId;
                        _answerRepository.Insert(answer);
                     //   _moduleQuestionRepository.AddNewQuestionToModule(moduleId, questionId, question.Id); ????
                    }
                }
            }

            return null;
        }

        public void DeleteFormById(int formId)
        {
            var formModules = _formModuleRepository.GetAllModulesByFormId(formId);

            foreach (var module in formModules)
            {
                var moduleQuestions = _moduleQuestionRepository.GetAllQuestionsByModuleId(module.IdModule);

                foreach (var question in moduleQuestions)
                {
                    _questionCustomServices.DeleteCustomQuestion(question.Id);
                    _moduleQuestionRepository.DeleteQuestionFromModule(module.IdModule, question.Id);
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

        //
        public IEnumerable<FormDetailDto> GetAllForsWithAllModules()
        {
            var results = _formRepository.AllForms();

            return _mapper.Map<IEnumerable<FormDetailDto>>(results);
        }
    }
}
