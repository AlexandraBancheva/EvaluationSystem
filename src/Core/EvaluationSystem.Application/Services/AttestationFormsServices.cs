﻿using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.AttestationForms;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Models.AttestationQuestions;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Services
{
    public class AttestationFormsServices : IAttestationFormsServices
    {
        private readonly IMapper _mapper;
        private readonly IAttestationFormRepository _attestationFormRepository;
        private readonly IAttestationModuleRepository _attestationModuleRepository;
        private readonly IAttestationAnswerRepository _attestationAnswerRepository;
        private readonly IAttestationModuleQuestionRepository _attestationModuleQuestionRepository;
        private readonly IAttestationFormModuleRepository _attestationFormModuleRepository;
        private readonly IAttestationRepository _attestationRepository;
        private readonly IAttestationQuestionRepository _attestationQuestionRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IAttestationQuestionsServices _attestationQuestionsServices;

        public AttestationFormsServices(IAttestationFormRepository attestationFormRepository, 
                                        IAttestationModuleRepository attestationModuleRepository,
                                        IAttestationModuleQuestionRepository attestationModuleQuestionRepository,
                                        IAttestationAnswerRepository attestationAnswerRepository,
                                        IAttestationFormModuleRepository attestationFormModuleRepository,
                                        IAttestationQuestionRepository attestationQuestionRepository,
                                        IUserAnswerRepository userAnswerRepository,
                                        IAttestationQuestionsServices attestationQuestionsServices, 
                                        IAttestationRepository attestationRepository,
                                        IMapper mapper)
        {
            _attestationFormRepository = attestationFormRepository;
            _attestationModuleRepository = attestationModuleRepository;
            _attestationModuleQuestionRepository = attestationModuleQuestionRepository;
            _attestationAnswerRepository = attestationAnswerRepository;
            _attestationFormModuleRepository = attestationFormModuleRepository;
            _attestationRepository = attestationRepository;
            _attestationQuestionRepository = attestationQuestionRepository;
            _userAnswerRepository = userAnswerRepository;
            _attestationQuestionsServices = attestationQuestionsServices;
            _mapper = mapper;
        }

        public int CreateNewForm(CreateFormDto form)
        {
            var currentForm = _mapper.Map<AttestationFormDto>(form);
            var attestationFormId = _attestationFormRepository.Insert(_mapper.Map<AttestationForm>(currentForm));
            currentForm.Id = attestationFormId;

            foreach (var module in currentForm.Modules)
            {
                var attestationModuleId = _attestationModuleRepository.Insert(_mapper.Map<AttestationModule>(module));
                module.Id = attestationModuleId;
                _attestationFormModuleRepository.AddModuleInForm(attestationFormId, attestationModuleId, module.ModulePosition);

                var attestationQuestions = module.Questions;

                foreach (var question in attestationQuestions)
                {
                    var newAttestationQuestionId = _attestationQuestionsServices.CreateNewQuestion(attestationModuleId, question.QuestionPosition, _mapper.Map<CreateQuestionDto>(question));
                    question.Id = newAttestationQuestionId;
                    var attestationAnswers = question.Answers;
                    foreach (var answer in attestationAnswers)
                    {
                        if (answer != null)
                        {
                            answer.IdQuestion = newAttestationQuestionId;
                            _attestationAnswerRepository.Insert(_mapper.Map<AttestationAnswer>(answer));
                        }     
                    }
                }
            }
            return attestationFormId;
        }

        public void DeleteFormFromAttestation(int formId)
        {
            var formModules = _attestationFormModuleRepository.GetAllModulesByFormId(formId);

            foreach (var module in formModules)
            {
                var moduleQuestions = _attestationModuleQuestionRepository.GetAllQuestionIdsByModuleId(module.IdModule);

                foreach (var question in moduleQuestions)
                {
                    _attestationQuestionsServices.DeleteAttestationQuestion(question.IdQuestion);
                    _attestationModuleQuestionRepository.DeleteQuestionFromModule(module.IdModule, question.IdQuestion);
                }

                _attestationFormModuleRepository.DeleteModuleFromForm(formId, module.IdModule);
                _attestationModuleRepository.DeleteAttestatationModule(module.IdModule);
            }

            _attestationFormRepository.DeleteAttestationForm(formId);
        }

        public ICollection<FormDetailDto> GetFormById(int attestationId)
        {
            var attestation = _attestationRepository.GetById(attestationId);
            if (attestation == null)
            {
                throw new InvalidOperationException("Invalid attestation id!");
            }

            var results = _attestationFormRepository.GetAllByFormId(attestation.IdForm);
            ModuleInFormDto tempModule = new ModuleInFormDto();
            QuestionInModuleDto tempQuestion = new QuestionInModuleDto();

            foreach (var form in results)
            {
                foreach (var module in form.Modules)
                {
                    if (module == null)
                    {
                        break;
                    }

                    if (tempModule.Name == module.Name)
                    {
                        form.Modules.Remove(module);
                    }
                    else
                    {
                        tempModule = module;
                    }

                    foreach (var question in module.Questions)
                    {
                        if (question == null)
                        {
                            break;
                        }
                        if (tempQuestion.Name == question.Name)
                        {
                            module.Questions.Remove(question);
                        }
                        else
                        {
                            tempQuestion = question;
                        }
                    }
                }
            }

            var res = _mapper.Map<ICollection<FormDetailDto>>(results);
            return res;
        }

        public void UpdateUserAnswer(int attestationId, AttestationQuestionUpdateDto model)
        {
            var userAnswer = _userAnswerRepository.GetUserAnswerByAttestationId(attestationId);

            if (userAnswer.IdAttestationQuestion == model.AttestationQuestionId)
            {
                var question = _attestationQuestionRepository.GetById(userAnswer.IdAttestationQuestion);
                if (question.Type == QuestionType.TextField)
                {
                    userAnswer.TextAnswer = model.AnswerText;
                    userAnswer.IdAttestationAnswer = null;
                    _userAnswerRepository.Update(userAnswer);
                }
                else if (question.Type == QuestionType.NumericalOptions || question.Type == QuestionType.RadioButtons)
                {
                    if (model.AnswerIds.Count != 1)
                    {
                        throw new InvalidOperationException("Only one answer");
                    }
                    else
                    {
                        userAnswer.IdAttestationAnswer = 0;
                        foreach (var answer in model.AnswerIds)
                        {
                            userAnswer.TextAnswer = null;
                            userAnswer.IdAttestationAnswer = answer;
                            _userAnswerRepository.Update(userAnswer);
                        }
                    }
                }
                else if (question.Type == QuestionType.CheckBoxes)
                {
                    foreach (var answer in model.AnswerIds)
                    {
                        userAnswer.TextAnswer = null;
                        userAnswer.IdAttestationAnswer = answer;
                        _userAnswerRepository.Update(userAnswer);
                    }
                }
            }
        }

        public static bool CheckIfFormNameExists(string formName, IAttestationFormRepository attestationFormRepository)
        {
            var allNames = attestationFormRepository.GetAllFormNames();

            foreach (var name in allNames)
            {
                if (name.Name == formName)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
