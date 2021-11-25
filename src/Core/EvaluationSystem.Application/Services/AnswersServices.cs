using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using System;

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

        public void AddNewAnswer(int questionId, AddNewAnswerDto model)
        {
            var isExist = _questionRepository.GetById(questionId);
            if (isExist == null)
            {
                throw new InvalidOperationException($"Question with this id {questionId} do not exist!");
            }
            var current = _mapper.Map<AnswerTemplate>(model);
            current.IdQuestion = questionId;
            var id = _answerRepository.Insert(current);
            current.Id = id;
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

         //   return _mapper.Map<AnswerDetailDto>(entity);
        }
    }
}
