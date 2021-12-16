using System;
using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Services
{
    public class FormsServices : IFormsServices
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormModuleRepository _formModuleRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IModulesServices _modulesServices;
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
            IModulesServices modulesServices,
            IModuleQuestionRepository moduleQuestionRepository,
            ICustomQuestionsServices questionCustomServices,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository, 
            ICustomQuestionsRepository customQuestionsRepository)
        {
            _formRepository = formRepository;
            _formModuleRepository = formModuleRepository;
            _moduleRepository = moduleRepository;
            _modulesServices = modulesServices;
            _moduleQuestionRepository = moduleQuestionRepository;
            _customQuestionsRepository = customQuestionsRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _questionCustomServices = questionCustomServices;
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
                //var isExistModuleName = CheckIfModuleNameExists(module.Name, _moduleRepository);
                //if (isExistModuleName == false)
                //{
                //    throw new InvalidOperationException($"The module name '{module.Name}' already exists.");
                //}

                var moduleId = _moduleRepository.Insert(_mapper.Map<ModuleTemplate>(module));

                _formModuleRepository.AddNewModuleInForm(formId, moduleId, module.ModulePosition);

                var questions = module.Questions;

                foreach (var question in questions)
                {
                    //var isExistQuestionName = CheckIfQuestionNameExists(question.Name, _questionRepository);
                    //if (isExistQuestionName == false)
                    //{
                    //    throw new InvalidOperationException($"The question name '{question.Name}' already exists.");
                    //}
                    var questionId = _questionCustomServices.CreateNewQuestion(moduleId, question.QuestionPosition, _mapper.Map<CreateQuestionDto>(question));

                    var answers = question.Answers;
                    foreach (var answer in answers)
                    {
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

        // Problem with repeated code!!!
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

        public static bool CheckIfModuleNameExists(string moduleName, IModuleRepository moduleRepository)
        {
            var allNames = moduleRepository.GetAllModuleNames();
            foreach (var name in allNames)
            {
                if (name.Name == moduleName)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckIfQuestionNameExists(string questionName, IQuestionRepository questionRepository)
        {
            var allNames = questionRepository.GetAllQuestionNames();
            foreach (var name in allNames)
            {
                if (name.Name == questionName)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
