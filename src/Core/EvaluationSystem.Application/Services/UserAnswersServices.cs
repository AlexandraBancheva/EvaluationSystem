using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.UserAnswers;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using System;

namespace EvaluationSystem.Application.Services
{
    public class UserAnswersServices : IUserAnswersServices
    {
        private IUser _currentUser;
        private readonly IUserAnswerRepository _userAnswerRepository;

        public UserAnswersServices(IUser currentUser, IUserAnswerRepository userAnswerRepository)
        {
            _currentUser = currentUser;
            _userAnswerRepository = userAnswerRepository;
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
                    IdAttestationQuestion = body.AttestationQuestionId,
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
                    attestationAnswer.IdAttestationAnswer = null;
                    attestationAnswer.TextAnswer = body.AnswerText;
                    _userAnswerRepository.Insert(attestationAnswer);
                }
            }

            _userAnswerRepository.ChangeStatusToDone(model.IdAttestation, _currentUser.Id);
        }
    }
}