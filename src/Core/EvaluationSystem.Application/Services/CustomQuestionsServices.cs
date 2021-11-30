using System;
using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Services
{
    public class CustomQuestionsServices : ICustomQuestionsServices
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IModuleQuestionsServices _moduleQuestionsServices;
        private readonly IMapper _mapper;

        public CustomQuestionsServices(IQuestionRepository questionRepository, IModuleQuestionsServices  moduleQuestionsServices, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _moduleQuestionsServices = moduleQuestionsServices;
            _mapper = mapper;
        }

        public int CreateNewQuestion(int moduleId, int position, CreateQuestionDto model)
        {
            var currentQuestion = _mapper.Map<QuestionTemplate>(model);
            currentQuestion.DateOfCreation = DateTime.UtcNow;
            currentQuestion.IsReusable = false;
            var questionId = _questionRepository.Insert(currentQuestion);
            _moduleQuestionsServices.AddQuestionToModule(moduleId, questionId, position);

            return questionId; //GetQuestionById(questionId);
        }

        public QuestionDetailDto GetQuestionById(int questionId)
        {
            var currentEntity = _questionRepository.GetById(questionId);

            if (currentEntity == null)
            {
                return null;
            }

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }
    }
}
