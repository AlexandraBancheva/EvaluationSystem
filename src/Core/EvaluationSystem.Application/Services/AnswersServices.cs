﻿using System;
using AutoMapper;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Services
{
    public class AnswersServices : IAnswersServices
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public AnswersServices(IAnswerRepository answerRepository, 
                               IQuestionRepository questionRepository, 
                               IMapper mapper)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public void AddNewAnswer(int questionId, AddListAnswers model)
        {
            var isExist = _questionRepository.GetById(questionId);
            if (isExist == null)
            {
                throw new InvalidOperationException($"Question with this id {questionId} do not exist!");
            }

            foreach (var answer in model.Answers)
            {
                var current = _mapper.Map<AnswerTemplate>(answer);
                current.IdQuestion = questionId;

                if (isExist.Type == QuestionType.NumericalOptions)
                {
                    var isNumeric = int.TryParse(current.AnswerText, out var answerNum);
                    if (!isNumeric)
                    {
                        throw new InvalidOperationException("Answer type must be numeric!");
                    }
                }

                var id = _answerRepository.Insert(current);
                current.Id = id;
            }

        }

        public void DeleteAnAnswer(int answerId)
        {
            var entity = _answerRepository.GetById(answerId);
            _answerRepository.Delete(entity);
        }

        public void UpdateAnswer(int questionId, int answerId, UpdateAnswerDto model)
        {
            var isExist = _questionRepository.GetById(questionId);
            if (isExist == null)
            {
                throw new InvalidOperationException($"Question with this id {questionId} do not exist!");
            }

            var entity = _mapper.Map<AnswerTemplate>(model);
            entity.Id = answerId;
            entity.IdQuestion = questionId;
            _answerRepository.Update(entity);
        }
    }
}
