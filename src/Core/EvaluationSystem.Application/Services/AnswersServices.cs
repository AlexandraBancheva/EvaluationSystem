﻿using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Services
{
    public class AnswersServices : IAnswersServices
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public AnswersServices(IAnswerRepository answerRepository, IQuestionRepository questionRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public AnswerDetailDto AddNewAnswer(int questionId, AddNewAnswerDto model)
        {
            var existedQuestion = _questionRepository.GetQuestionById(questionId);
            if (existedQuestion == null)
            {
                return null;
            }
            var currentAnswer = _mapper.Map<Answer>(model);
            _answerRepository.AddNewAnswer(currentAnswer);

            return _mapper.Map<AnswerDetailDto>(currentAnswer);
        }

        public void DeleteAnAnswer(int questionId, int answerId)
        {
            var existedQuestion = _questionRepository.GetQuestionById(questionId);
            if (existedQuestion == null)
            {
                throw new InvalidOperationException($"Question with {questionId} don't exist!");
            }
            _answerRepository.DeleteAnAnswer(questionId, answerId);
        }

        public IEnumerable<ListAnswersByQuestionId> GetAnswersByQuestionId(int questionId)
        {
            var existedQuestion = _questionRepository.GetQuestionById(questionId);
            if (existedQuestion == null)
            {
                return null;
            }
            var results = _answerRepository.GetAllAnswersByQuestionId(questionId);
            return _mapper.Map<IEnumerable<ListAnswersByQuestionId>>(results);
        }
    }
}
