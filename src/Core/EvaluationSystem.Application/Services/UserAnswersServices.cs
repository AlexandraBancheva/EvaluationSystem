using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.AttestationForms;
using EvaluationSystem.Application.Models.UserAnswers;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Domain.Enums;

namespace EvaluationSystem.Application.Services
{
    public class UserAnswersServices : IUserAnswersServices
    {
        private IUser _currentUser;
        private IMapper _mapper;
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IAttestationRepository _attestationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttestationFormsServices _attestationFormsServices;

        public UserAnswersServices(IUser currentUser, 
                                   IMapper mapper,
                                   IUserAnswerRepository userAnswerRepository, 
                                   IAttestationRepository attestationRepository,
                                   IUserRepository userRepository,
                                   IAttestationFormsServices attestationFormsServices)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _userAnswerRepository = userAnswerRepository;
            _attestationRepository = attestationRepository;
            _userRepository = userRepository;
            _attestationFormsServices = attestationFormsServices;
        }

        public void Create(CreateUserAnswerDto model)
        {
            foreach (var body in model.Body)
            {
                var attestationAnswer = new UserAnswer
                {
                    IdAttestation = model.IdAttestation,
                    IdUserParticipant = _currentUser.Id,
                    IdAttestationModule = body.AttestationModuleId,
                    IdAttestationQuestion = body.AttestationQuestionId
                };

                if (body.AnswerIds.Count != 0)
                {
                    foreach (var answerId in body.AnswerIds)
                    {
                        attestationAnswer.IdAttestationAnswer = answerId;
                        attestationAnswer.TextAnswer = null;
                        _userAnswerRepository.Insert(attestationAnswer);
                    }
                }
                else
                {
                    if (body.AnswerText == null || body.AnswerText == "")
                    {
                        throw new Exception("AnswerText is empty!");
                    }
                    attestationAnswer.IdAttestationAnswer = 0;
                    attestationAnswer.TextAnswer = body.AnswerText;
                    _userAnswerRepository.AddAnswerLikeATextField(attestationAnswer.IdAttestation, attestationAnswer.IdUserParticipant, attestationAnswer.IdAttestationModule, attestationAnswer.IdAttestationQuestion, attestationAnswer.TextAnswer);
                }
            }
            _userAnswerRepository.ChangeStatusToDone(model.IdAttestation, _currentUser.Id);
        }

        public ICollection<AttestationFormDetailDto> GetAttestationAnswerByUser(int attestationId, string userEmail)
        {
            var attestation = _attestationRepository.GetById(attestationId);
            var form = _attestationFormsServices.GetFormById(attestationId);
            var participant = _userRepository.GetUserByEmail(userEmail);


            var attestationAnswers = _userAnswerRepository.GetAllAnswersByUser(attestationId, participant.Id);
            var resultForm = _mapper.Map<ICollection<AttestationFormDetailDto>>(form);
            if (attestationAnswers.Count == 0)
            {
                throw new InvalidOperationException("Attestation has not been decided yet!");
            }
            else
            {
                foreach (var body in attestationAnswers)
                {
                    foreach (var currForm in resultForm)
                    {
                        if (currForm.Id == attestation.IdForm)
                        {
                            foreach (var currentModule in currForm.Modules)
                            {
                                foreach (var currentQuestion in currentModule.Questions)
                                {
                                    if (currentQuestion.Type == QuestionType.TextField)
                                    {
                                        currentQuestion.TextAnswer = body.TextAnswer;
                                    }
                                    else if (currentQuestion.Type == QuestionType.NumericalOptions || currentQuestion.Type == QuestionType.RadioButtons)
                                    {
                                        foreach (var answer in currentQuestion.Answers)
                                        {
                                            if (answer.IdAnswer == body.IdAttestationAnswer)
                                            {
                                                answer.IsAnswered = true;
                                            }
                                        }
                                    }
                                    else if (currentQuestion.Type == QuestionType.CheckBoxes)
                                    {
                                        foreach (var answer in currentQuestion.Answers)
                                        {
                                            if (answer.IdAnswer == body.IdAttestationAnswer)
                                            {
                                                answer.IsAnswered = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return resultForm;
        }

        public void DeleteUserAnswer(int attestationId)
        {
            _userAnswerRepository.DeleteUserAnswerByAttestationId(attestationId);
        }
    }
}