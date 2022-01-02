using System;
using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Services
{
    public class AttestationQuestionsServices : IAttestationQuestionsServices
    {
        private readonly IMapper _mapper;
        private readonly IAttestationQuestionRepository _attestationQuestionRepository;
        private readonly IAttestationModuleQuestionRepository _attestationModuleQuestionRepository;

        public AttestationQuestionsServices(IMapper mapper, IAttestationQuestionRepository attestationQuestionRepository,
            IAttestationModuleQuestionRepository attestationModuleQuestionRepository)
        {
            _mapper = mapper;
            _attestationQuestionRepository = attestationQuestionRepository;
            _attestationModuleQuestionRepository = attestationModuleQuestionRepository;
        }

        public CustomQuestionDetailDto CreateNewQuestion(int moduleId, int position, CreateQuestionDto model) // Maybe return int???
        {
            var currentQuestion = _mapper.Map<AttestationQuestion>(model);
            currentQuestion.IsReusable = false;
            currentQuestion.DateOfCreation = DateTime.UtcNow;
            int questionId = _attestationQuestionRepository.Insert(currentQuestion);
            _attestationModuleQuestionRepository.AddNewQuestionToModule(moduleId, questionId, position);

            return GetCustomQuestionById(questionId);
        }

        public void DeleteAttestationQuestion(int questionId)
        {
            _attestationQuestionRepository.DeleteQuestion(questionId);
        }

        // It is not necessary
        public CustomQuestionDetailDto GetCustomQuestionById(int questionId)
        {
            var currentEntity = _attestationQuestionRepository.GetById(questionId);

            if (currentEntity == null)
            {
                return null;
            }

            return _mapper.Map<CustomQuestionDetailDto>(currentEntity);
        }
    }
}
