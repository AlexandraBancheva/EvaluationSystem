using System;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Services
{
    public class CustomQuestionsServices : ICustomQuestionsServices
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICustomQuestionsRepository _customQuestionsRepository;
        private readonly IModuleQuestionsServices _moduleQuestionsServices;
        private readonly IMapper _mapper;

        public CustomQuestionsServices(IQuestionRepository questionRepository, 
                                        IModuleQuestionsServices  moduleQuestionsServices, 
                                        ICustomQuestionsRepository customQuestionsRepository, 
                                        IMapper mapper)
        {
            _questionRepository = questionRepository;
            _customQuestionsRepository = customQuestionsRepository;
            _moduleQuestionsServices = moduleQuestionsServices;
            _mapper = mapper;
        }

        public CustomQuestionDetailDto CreateNewQuestion(int moduleId, int position, CreateQuestionDto model)
        {
            var currentQuestion = _mapper.Map<QuestionTemplate>(model);
            currentQuestion.DateOfCreation = DateTime.UtcNow;
            currentQuestion.IsReusable = false;
            var questionId = _customQuestionsRepository.Insert(currentQuestion);
            _moduleQuestionsServices.AddQuestionToModule(moduleId, questionId, position);

            return GetCustomQuestionById(questionId);
        }

        public void DeleteCustomQuestion(int questionId)
        {
            var entity = GetCustomQuestionById(questionId);
            if (entity.IsReusable == true)
            {
                _customQuestionsRepository.RemovedQuestion(questionId);
            }
            else
            {
                _questionRepository.DeleteTemplateQuestion(questionId);
            }
        }

        public CustomQuestionDetailDto GetCustomQuestionById(int questionId)
        {
            var currentEntity = _customQuestionsRepository.GetCustomById(questionId);
            if (currentEntity == null)
            {
                return null;
            }

            return _mapper.Map<CustomQuestionDetailDto>(currentEntity);
        }

        //Repeated code!
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
