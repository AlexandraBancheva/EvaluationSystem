using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

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

        public QuestionDetailDto GetQuestionById(int questionId)
        {
            var currentEntity =  _questionRepository.GetById(questionId);

            if (currentEntity == null)
            {
                return null;
            }

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto CreateNewQuestion(CreateQuestionDto model)
        {
            var currentEntity = _mapper.Map<QuestionTemplate>(model);
            currentEntity.DateOfCreation = DateTime.UtcNow;
            _questionRepository.Insert(currentEntity);

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model)
        {
            var isExist = _questionRepository.GetById(questionId);

            if (isExist == null)
            {
                return null;
            }

            var current = _mapper.Map<QuestionTemplate>(model);
            current.Id = questionId;
            current.DateOfCreation = DateTime.UtcNow;
            _questionRepository.Update(current);

            return _mapper.Map<QuestionDetailDto>(current);
        }

        public void DeleteQuestion(int questionId)
        {
            var entity = _questionRepository.GetById(questionId);
            _questionRepository.Delete(entity);
        }

        public IEnumerable<ListQuestionsAnswersDto> GetAllQuestionsWithTheirAnswers()
        {
            var questions = _questionRepository.GetAllQuestionsWithAnswers();
            var questionsAll = _mapper.Map<IEnumerable<ListQuestionsAnswersDto>>(questions);

            return questionsAll;
        }
    }
}
