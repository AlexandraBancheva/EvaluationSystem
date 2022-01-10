using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Services
{
    public class AttestationAnswersServices : IAttestationAnswersServices
    {
        private readonly IAttestationAnswerRepository _attestationAnswerRepository;
        private IMapper _mapper;

        public AttestationAnswersServices(IAttestationAnswerRepository attestationAnswerRepository, IMapper mapper)
        {
            _attestationAnswerRepository = attestationAnswerRepository;
            _mapper = mapper;
        }

        public ICollection<AnswerListDto> CreateAnswer(int questionId, AddListAnswers model)
        {
            var isExist = _attestationAnswerRepository.GetById(questionId);
            if (isExist == null)
            {
                throw new InvalidOperationException($"Question with this id {questionId} do not exist!");
            }

            AttestationAnswer current = new AttestationAnswer();
            foreach (var answer in model.Answers)
            {
                current = _mapper.Map<AttestationAnswer>(answer);
                current.IdQuestion = questionId;

                // CHECKS!
                //if (isExist.Type == QuestionType.NumericalOptions)
                //{
                //    var isNumeric = int.TryParse(current.AnswerText, out var answerNum);
                //    if (!isNumeric)
                //    {
                //        throw new InvalidOperationException("Answer type must be numeric!");
                //    }
                //}

                var id = _attestationAnswerRepository.Insert(current);
                current.Id = id;
            }

            var allAnswers = _attestationAnswerRepository.GetAllByQuestionId(questionId);
            return _mapper.Map<ICollection<AnswerListDto>>(allAnswers);
        }

        public void DeleteAttestationAnswer(int answerId)
        {
            var currentAnswer = _attestationAnswerRepository.GetById(answerId);
            _attestationAnswerRepository.Delete(currentAnswer);
        }
    }
}
