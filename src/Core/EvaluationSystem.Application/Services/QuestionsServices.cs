using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Services
{
    public class QuestionsServices : IQuestionsServices
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionsServices(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public IEnumerable<ListQuestionsDto> GetAll()
        {
            var allQuestions = _questionRepository.AllQuestions();
            var result = _mapper.Map<IEnumerable<ListQuestionsDto>>(allQuestions);

            return result;
        }

        public QuestionDetailDto GetQuestionById(int questionId)
        {
            var currentEntity =  _questionRepository.GetQuestionById(questionId);

            if (currentEntity == null)
            {
                return null;
            }

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto CreateNewQuestion(CreateQuestionDto model)
        {
            var currentEntity = _mapper.Map<Question>(model);
            _questionRepository.CreateNewQuestion(currentEntity);

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model)
        {
            var currentEntity = _questionRepository.GetQuestionById(questionId);
            if (currentEntity == null)
            {
                return null;
            }

            var updatedModel = _mapper.Map<Question>(model);
            var result = _questionRepository.UpdateCurrentQuestion(updatedModel);

            return _mapper.Map<QuestionDetailDto>(result);
        }

        public void DeleteQuestion(int questionId)
        {
            var entity = _questionRepository.GetQuestionById(questionId);
            if (entity == null)
            {
                throw new InvalidOperationException($"Question with {questionId} don't exist!");
            }
            _questionRepository.DeleteQuestion(questionId);
        }
    }
}
