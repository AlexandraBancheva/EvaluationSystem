using AutoMapper;
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
            var isExist = _questionRepository.GetQuestionById(questionId);
            if (isExist == null)
            {
                return null;
            }
            var current = _mapper.Map<Answer>(model);
            _answerRepository.AddNewAnswer(questionId, current);
            
            return _mapper.Map<AnswerDetailDto>(current);
        }

        public void DeleteAnAnswer(int questionId, int answerId)
        {
            var isExistQuestion = _questionRepository.GetQuestionById(questionId);
            if (isExistQuestion != null)
            {
                _answerRepository.DeleteAnAnswer(answerId);
            }
        }

        public IEnumerable<QuestionByIdWithAnswersListDto> GetAnswersByQuestionId(int questionId)
        {
            var existedQuestion = _questionRepository.GetQuestionById(questionId);
            if (existedQuestion == null)
            {
                return null;
            }
            var results = _answerRepository.GetAllAnswersByQuestionId(questionId);
            return null;//_mapper.Map<IEnumerable<QuestionByIdWithAnswersListDto>(results);
        }
    }
}
