using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using System.Linq;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

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

        // If module is null
        public ICollection<FormDetailDto> CreateNewForm(CreateFormDto form)
        {
            var currentForm = _mapper.Map<FormTemplateDto>(form);

            var formId = _formRepository.Insert(_mapper.Map<FormTemplate>(currentForm));

            foreach (var module in currentForm.Modules)
            {
                var moduleId = _moduleRepository.Insert(_mapper.Map<ModuleTemplate>(module));
                _formModuleRepository.AddNewModuleInForm(formId, moduleId, currentForm.Position);

                var questions = module.Questions;

                foreach (var question in questions)
                {

                    var questionId = _questionCustomServices.CreateNewQuestion(moduleId, module.Position, _mapper.Map<CreateQuestionDto>(question));

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

        //
        public ICollection<FormDetailDto> GetFormById(int formId)
        {
            var results = _formRepository.GetAllWithFormId(formId);
            ModuleInFormDto tempModule = new ModuleInFormDto();
            QuestionInModuleDto tempQuestion = new QuestionInModuleDto();

            foreach (var form in results)
            {
                foreach (var module in form.Modules)
                {
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
            entity.Id = formId;
            _formRepository.Update(entity);

            return _mapper.Map<UpdatedFormDto>(_formRepository.GetById(formId));
        }

        public IEnumerable<FormDetailDto> GetAllForsWithAllModules()
        {
            var results = _formRepository.AllForms();

            return _mapper.Map<IEnumerable<FormDetailDto>>(results);
        }
    }
}
