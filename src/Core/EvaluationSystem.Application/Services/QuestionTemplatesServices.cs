using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Services
{
    public class QuestionTemplatesServices : IQuestionTemplatesServices
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionTemplatesServices(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
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

        public QuestionDetailDto CreateNewQuestion(CreateQuestionDto model)
        {
            var currentEntity = _mapper.Map<QuestionTemplate>(model);
            currentEntity.DateOfCreation = DateTime.UtcNow;
            currentEntity.IsReusable = true;
            var newEntityId = _questionRepository.Insert(currentEntity);

            return GetQuestionById(newEntityId);
        }

        public QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model)
        {
            var isExist = _questionRepository.GetById(questionId);

            if (isExist == null)
            {
                return null;
            }

            var currentEntityForUpdate = _mapper.Map<QuestionTemplate>(model);
            var currentNameEntity = isExist.Name;
            var updatedEntity = new QuestionTemplate();
            if (currentNameEntity != currentEntityForUpdate.Name)
            {
                updatedEntity = new QuestionTemplate
                {
                    Id = questionId,
                    Name = currentEntityForUpdate.Name,
                    Type = isExist.Type,
                    IsReusable = isExist.IsReusable,
                    DateOfCreation = DateTime.UtcNow,
                    Answers = isExist.Answers,
                };
                _questionRepository.Update(updatedEntity);
            }
            return _mapper.Map<QuestionDetailDto>(updatedEntity);
        }

        public void DeleteQuestion(int questionId)
        {
            _questionRepository.DeleteTemplateQuestion(questionId);
        }

        public IEnumerable<ListQuestionsAnswersDto> GetAllQuestionsWithTheirAnswers()
        {
            var questions = _questionRepository.GetAllQuestionsWithAnswers();
            var questionsAll = _mapper.Map<IEnumerable<ListQuestionsAnswersDto>>(questions);

            return questionsAll;
        }

        public IEnumerable<ListQuestionsAnswersDto> GetAllAnswersByQuestionId(int questionId)
        {
            var questions = _questionRepository.GetAllById(questionId);

            return _mapper.Map<IEnumerable<ListQuestionsAnswersDto>>(questions);
        }

        public IEnumerable<ListQuestionsAnswersDto> GetAllQuestionTemplatesWithTheirAnswers()
        {
            // throw new NotImplementedException();
            var allQuestionTemplates = _questionRepository.GetAllQuestionTemplates();

            return _mapper.Map<IEnumerable<ListQuestionsAnswersDto>>(allQuestionTemplates);
        }
    }
}