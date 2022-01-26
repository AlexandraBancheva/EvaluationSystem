using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Services
{
    public class ModuleQuestionsServices : IModuleQuestionsServices
    {
        private readonly IModuleQuestionRepository _moduleQuestionRepository;
        private readonly ICustomQuestionsRepository _customQuestionsRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public ModuleQuestionsServices(IModuleQuestionRepository moduleQuestionRepository,
                                       ICustomQuestionsRepository customQuestionsRepository,
                                       IQuestionRepository questionRepository,
                                       IMapper mapper)
        {
            _moduleQuestionRepository = moduleQuestionRepository;
            _customQuestionsRepository = customQuestionsRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public void AddQuestionToModule(int moduleId, int questionId, int position)
        {
            _moduleQuestionRepository.AddNewQuestionToModule(moduleId, questionId, position);
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            var question = _customQuestionsRepository.GetCustomById(questionId);
            var entity = _mapper.Map<CustomQuestionDetailDto>(question);
            if (entity.IsReusable == true)
            {
                _customQuestionsRepository.RemovedQuestion(questionId);
            }
            else
            {
                _questionRepository.DeleteTemplateQuestion(questionId);
            }
            _moduleQuestionRepository.DeleteQuestionFromModule(moduleId, questionId);
        }
         
        public IEnumerable<ListModulesQuestionsDto> GetAllModulesWithAllQuestions()
        {
            var modules = _moduleQuestionRepository.GetModuleWithAllQuestions();
            var allModules = _mapper.Map<IEnumerable<ListModulesQuestionsDto>>(modules);

            return allModules;
        }

        public IEnumerable<ListModulesQuestionsDto> GetModuleWithAllQuestions(int moduleId)
        {
            var moduleQuestions = _moduleQuestionRepository.GetAllQuestionsByModuleId(moduleId);
            var res = _mapper.Map<IEnumerable<ListModulesQuestionsDto>>(moduleQuestions);

            return res;
        }
    }
}
