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
                foreach (var attModule in body.AttestationModuleIds)
                {
                    foreach (var attQuestion in attModule.QuestionIds)
                    {
                        var attestationAnswer = new UserAnswer
                        {
                            IdAttestation = model.IdAttestation,
                            IdUserParticipant = _currentUser.Id,
                            IdAttestationModule = attModule.AttestationModuleId,
                            IdAttestationQuestion = attQuestion.AttestationQuestionId,
                        };

                        if (attQuestion.Answer.Count != 0)
                        {
                            foreach (var answer in attQuestion.Answer)
                            {
                                if (answer.AnswerId != 0)
                                {
                                        attestationAnswer.IdAttestationAnswer = answer.AnswerId;
                                        attestationAnswer.TextAnswer = null;
                                        _userAnswerRepository.Insert(attestationAnswer);
                                }
                                else
                                {
                                    if (answer.TextAnswer != null || answer.TextAnswer != string.Empty)
                                    {
                                        attestationAnswer.TextAnswer = answer.TextAnswer;
                                        attestationAnswer.IdAttestationAnswer = null;
                                        _userAnswerRepository.Insert(attestationAnswer);
                                    }
                                    else
                                    {
                                        throw new Exception("Text answer cannot be empty.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _userAnswerRepository.ChangeStatusToDone(model.IdAttestation, _currentUser.Id);
        }

        public ICollection<AttestationFormDetailDto> GetAttestationAnswerByUser(int attestationId, string userEmail)
        {
            var attestation = _attestationRepository.GetById(attestationId);
            var form = _attestationFormsServices.GetFormById(attestation.IdForm);
            var participant = _userRepository.GetUserByEmail(userEmail);


            var attestationAnswers = _userAnswerRepository.GetAllAnswersByUser(attestationId, participant.Id);
            var resultForm = _mapper.Map<ICollection<AttestationFormDetailDto>>(form); 

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
                                if (currentQuestion.Type == (int)QuestionType.TextField)
                                {
                                    currentQuestion.TextAnswer = body.TextAnswer;
                                }
                                else
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

            return resultForm;
        }

        public void DeleteUserAnswer(int attestationId)
        {
            _userAnswerRepository.DeleteUserAnswerByAttestationId(attestationId);
        }
    }
}