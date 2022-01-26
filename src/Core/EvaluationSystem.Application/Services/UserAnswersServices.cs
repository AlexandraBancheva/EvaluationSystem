using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.AttestationForms;
using EvaluationSystem.Application.Models.UserAnswers;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Application.Models.AttestationQuestions;
using System.Linq;

namespace EvaluationSystem.Application.Services
{
    public class UserAnswersServices : IUserAnswersServices
    {
        private readonly IUser _currentUser;
        private readonly IMapper _mapper;
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
            var checkStatus = _userAnswerRepository.CheckParticipantStatusIsDone(model.IdAttestation);
            if (checkStatus.Status == "Done")
            {
                throw new InvalidOperationException("Your attestation has been filled in. You cannot change answers!");
            }
            else
            {
                var answeredAttestation = GetAttestationAnswerByUser(model.IdAttestation, _currentUser.Email);

                foreach (var form in answeredAttestation)
                {
                    foreach (var module in form.Modules)
                    {
                        foreach (var body in model.Body)
                        {
                            if (module.IdModule == body.AttestationModuleId)
                            {
                                var userAnswer = new UserAnswer
                                {
                                    IdAttestation = model.IdAttestation,
                                    IdUserParticipant = _currentUser.Id,
                                    IdAttestationModule = body.AttestationModuleId,
                                    IdAttestationQuestion = body.AttestationQuestionId,
                                };
                                if (module.IdModule == body.AttestationModuleId)
                                {
                                    foreach (var question in module.Questions)
                                    {
                                        if (question.IdQuestion == body.AttestationQuestionId)
                                        {
                                            var updateUserAnswers = new AttestationQuestionUpdateDto();
                                            if (question.Type == QuestionType.TextField)
                                            {
                                                if (question.TextAnswer != body.AnswerText)
                                                {
                                                    updateUserAnswers = _mapper.Map<AttestationQuestionUpdateDto>(body);
                                                    _attestationFormsServices.UpdateUserAnswer(userAnswer.IdAttestation, updateUserAnswers);
                                                }
                                            }
                                            else
                                            {
                                                var flag = !question.Answers.Any(a => a.IsAnswered == true);
                                                if (flag)
                                                {
                                                    updateUserAnswers = _mapper.Map<AttestationQuestionUpdateDto>(body);
                                                    _attestationFormsServices.UpdateUserAnswer(userAnswer.IdAttestation, updateUserAnswers);
                                                }
                                                else
                                                {
                                                    foreach (var answer in question.Answers)
                                                    {
                                                        foreach (var answerId in body.AnswerIds)
                                                        {
                                                            if (answer.IdAnswer != answerId)
                                                            {
                                                                updateUserAnswers = _mapper.Map<AttestationQuestionUpdateDto>(body);
                                                                _attestationFormsServices.UpdateUserAnswer(userAnswer.IdAttestation, updateUserAnswers);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                var isAnswered = CheckIfAllQuestionsAreAnswered(model.IdAttestation, _currentUser.Email);
                if (isAnswered == false)
                {
                    throw new InvalidOperationException("Some questions are not answered!");
                }
                _userAnswerRepository.ChangeStatusToDone(model.IdAttestation, _currentUser.Id);
            }
        }

        public ICollection<AttestationFormDetailDto> GetAttestationAnswerByUser(int attestationId, string userEmail)
        {
            var attestation = _attestationRepository.GetById(attestationId);
            var form = _attestationFormsServices.GetFormById(attestationId);
            var participant = _userRepository.GetUserByEmail(userEmail);

            var attestationAnswers = _userAnswerRepository.GetAllAnswersByUser(attestationId, participant.Id);
            var resultForm = _mapper.Map<ICollection<AttestationFormDetailDto>>(form);

            foreach (var currForm in resultForm)
            {
                if (currForm.Id == attestation.IdForm)
                {
                    foreach (var currentModule in currForm.Modules)
                    {
                        foreach (var currentQuestion in currentModule.Questions)
                        {
                            foreach (var body in attestationAnswers)
                            {
                                if (currentQuestion.IdQuestion == body.IdAttestationQuestion)
                                {
                                    if (body.IdAttestationAnswer != null)
                                    {
                                        foreach (var answer in currentQuestion.Answers)
                                        {
                                            if (answer.IdAnswer == body.IdAttestationAnswer)
                                            {
                                                answer.IsAnswered = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        currentQuestion.TextAnswer = body.TextAnswer;
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

        private bool CheckIfAllQuestionsAreAnswered(int attestationId, string emailUser)
        {
            var getAttestationAnswers = GetAttestationAnswerByUser(attestationId, emailUser);
            foreach (var form in getAttestationAnswers)
            {
                foreach (var module in form.Modules)
                {
                    foreach (var question in module.Questions)
                    {
                        if (question.Type == QuestionType.TextField)
                        {
                            if (question.TextAnswer == null)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            var anyAnswers = question.Answers.Any(a => a.IsAnswered == true);
                            if (anyAnswers == false)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}