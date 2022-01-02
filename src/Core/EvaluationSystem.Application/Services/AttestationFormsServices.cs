﻿using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.AttestationForms;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using System;

namespace EvaluationSystem.Application.Services
{
    public class AttestationFormsServices : IAttestationFormsServices
    {
        private readonly IAttestationFormRepository _attestationFormRepository;
        private readonly IAttestationModuleRepository _attestationModuleRepository;
        private readonly IAttestationAnswerRepository _attestationAnswerRepository;
        private readonly IAttestationModuleQuestionRepository _attestationModuleQuestionRepository;
        private readonly IAttestationFormModuleRepository _attestationFormModuleRepository;
        private readonly IAttestationQuestionsServices _attestationQuestionsServices;
        private IMapper _mapper;

        public AttestationFormsServices(IAttestationFormRepository attestationFormRepository, 
                                        IAttestationModuleRepository attestationModuleRepository,
                                        IAttestationModuleQuestionRepository attestationModuleQuestionRepository,
                                        IAttestationAnswerRepository attestationAnswerRepository,
                                        IAttestationFormModuleRepository attestationFormModuleRepository,
                                        IAttestationQuestionsServices attestationQuestionsServices, 
                                        IMapper mapper)
        {
            _attestationFormRepository = attestationFormRepository;
            _attestationModuleRepository = attestationModuleRepository;
            _attestationModuleQuestionRepository = attestationModuleQuestionRepository;
            _attestationAnswerRepository = attestationAnswerRepository;
            _attestationFormModuleRepository = attestationFormModuleRepository;
            _attestationQuestionsServices = attestationQuestionsServices;
            _mapper = mapper;
        }

        public void CreateNewForm(CreateFormDto form)
        {
            var currentForm = _mapper.Map<AttestationFormDto>(form);
            var IsExist = CheckIfFormNameExists(currentForm.Name, _attestationFormRepository);

            if (IsExist == false)
            {
                throw new InvalidOperationException($"The form name '{currentForm.Name}' already exists.");
            }

            var attestationFormId = _attestationFormRepository.Insert(_mapper.Map<AttestationForm>(currentForm));

            foreach (var module in currentForm.Modules)
            {
                var attestationModuleId = _attestationModuleRepository.Insert(_mapper.Map<AttestationModule>(module));

                _attestationFormModuleRepository.AddModuleInForm(attestationFormId, attestationModuleId, module.ModulePosition);

                var attestationQuestions = module.Questions;

                foreach (var question in attestationQuestions)
                {
                    var newAttestationQuestion = _attestationQuestionsServices.CreateNewQuestion(attestationModuleId, question.QuestionPosition, _mapper.Map<CreateQuestionDto>(question));

                    var attestationAnswers = question.Answers;

                    foreach (var answer in attestationAnswers)
                    {
                        answer.IdQuestion = newAttestationQuestion.IdQuestion;
                        _attestationAnswerRepository.Insert(_mapper.Map<AttestationAnswer>(answer));
                    }
                }
            }
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
