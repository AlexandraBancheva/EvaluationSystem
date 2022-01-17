using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Services
{
    public class FormsServices : IFormsServices
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormModuleRepository _formModuleRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleQuestionRepository _moduleQuestionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ICustomQuestionsServices _questionCustomServices;
        private readonly IModulesServices _modulesServices;
        private readonly IMapper _mapper;

        public FormsServices(IMapper mapper, 
            IFormRepository formRepository,
            IFormModuleRepository formModuleRepository, 
            IModuleRepository moduleRepository,
            IModuleQuestionRepository moduleQuestionRepository,
            ICustomQuestionsServices questionCustomServices,
            IModulesServices modulesServices,
            IAnswerRepository answerRepository)
        {
            _formRepository = formRepository;
            _formModuleRepository = formModuleRepository;
            _moduleRepository = moduleRepository;
            _moduleQuestionRepository = moduleQuestionRepository;
            _answerRepository = answerRepository;
            _questionCustomServices = questionCustomServices;
            _modulesServices = modulesServices;
            _mapper = mapper;
        }


        public ICollection<FormDetailDto> CreateNewForm(CreateFormDto form)
        {
            var currentForm = _mapper.Map<FormTemplateDto>(form);
            var IsExist = CheckIfFormNameExists(currentForm.Name, _formRepository);

            if (IsExist == false)
            {
                throw new InvalidOperationException($"The form name '{currentForm.Name}' already exists.");
            }

            var formId = _formRepository.Insert(_mapper.Map<FormTemplate>(currentForm));

            foreach (var module in currentForm.Modules)
            {
                var mappedEntity = _mapper.Map<CreateModuleDto>(module);
                var newModule = _modulesServices.CreateModule(formId, mappedEntity);  // Fluent validation doesn't work!

                var questions = module.Questions;

                foreach (var question in questions)
                {
                    var mappedNewEntity = _mapper.Map<CreateQuestionDto>(question);
                    var questionNew = _questionCustomServices.CreateNewQuestion(newModule.Id, question.QuestionPosition, mappedNewEntity);
                    question.Id = questionNew.IdQuestion;
                    question.DateOfCreation = questionNew.DateOfCreation;
                    var answers = question.Answers;
                    foreach (var answer in answers)
                    {
                        answer.IdQuestion = question.Id;
                        var answerId =  _answerRepository.Insert(answer);
                        answer.Id = answerId;
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
                var moduleQuestions = _moduleQuestionRepository.GetAllQuestionIdsByModuleId(module.IdModule);

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

        public ICollection<FormDetailDto> GetFormById(int formId)
        {
            var results = _formRepository.GetAllByFormId(formId);
            ModuleInFormDto tempModule = new ModuleInFormDto();
            QuestionInModuleDto tempQuestion = new QuestionInModuleDto();

            foreach (var form in results)
            {
                foreach (var module in form.Modules)
                {
                    if (module == null)
                    {
                        break;
                    }

                    if (tempModule.Name == module.Name)
                    {
                        form.Modules.Remove(module);
                    }
                    else
                    {
                        tempModule = module;
                    }

                    foreach (var question in module.Questions)
                    {
                        if (question == null)
                        {
                            break;
                        }
                        if (tempQuestion.Name == question.Name)
                        {
                            module.Questions.Remove(question);
                        }
                        else
                        {
                            tempQuestion = question;
                        }
                    }
                }
            }

            var res = _mapper.Map<ICollection<FormDetailDto>>(results);
            return res;
        }

        public UpdatedFormDto UpdateCurrentForm(int formId, UpdateFormDto form)
        {
            var entity = _mapper.Map<FormTemplate>(form);
            var IsExist = CheckIfFormNameExists(entity.Name.ToString(), _formRepository);

            if (IsExist == false)
            {
                throw new InvalidOperationException($"The form name '{entity.Name}' already exists.");
            }
            entity.Id = formId;
            _formRepository.Update(entity);
            var res = _formRepository.GetById(formId);
            return _mapper.Map<UpdatedFormDto>(res);
        }

        public ICollection<FormDetailDto> GetAllForsWithAllModules()
        {
            var results = _formRepository.GetAllForms();
            ModuleInFormDto tempModule = new ModuleInFormDto();
            QuestionInModuleDto tempQuestion = new QuestionInModuleDto();

            foreach (var form in results)
            {
                foreach (var module in form.Modules)
                {
                    if (module == null)
                    {
                        break;
                    }

                    if (tempModule.Name == module.Name)
                    {
                        form.Modules.Remove(module);
                    }
                    else
                    {
                        tempModule = module;
                    }

                    foreach (var question in module.Questions)
                    {
                        if (question == null)
                        {
                            break;
                        }
                        if (tempQuestion.Name == question.Name)
                        {
                            module.Questions.Remove(question);
                        }
                        else
                        {
                            tempQuestion = question;
                        }
                    }
                }
            }

            return _mapper.Map<ICollection<FormDetailDto>>(results);
        }

        public static bool CheckIfFormNameExists(string formName, IFormRepository formRepository)
        {
            var allNames = formRepository.GetAllFormNames();

            foreach (var name in allNames)
            {
                if (name.Name == formName)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
