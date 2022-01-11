﻿using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Services
{
    public class AnswersServices : IAnswersServices
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IModuleRepository _modulesRepository;
        private readonly IFormModuleRepository _formModuleRepository;
        private readonly IFormsServices _formsServices;

        public AnswersServices(IAnswerRepository answerRepository, 
                               IQuestionRepository questionRepository,
                               IModuleRepository modulesRepository,
                               IFormModuleRepository formModuleRepository,
                               IFormsServices formsServices,
                               IMapper mapper)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _modulesRepository = modulesRepository;
            _formModuleRepository = formModuleRepository;
            _formsServices = formsServices;
            _mapper = mapper;
        }

        public ICollection<AnswerListDto> CreateAnswer(int questionId, AddListAnswers model)
        {
            var isExist = _questionRepository.GetById(questionId);
            if (isExist == null)
            {
                throw new InvalidOperationException($"Question with this id {questionId} do not exist!");
            }

            AnswerTemplate current = new AnswerTemplate();
            foreach (var answer in model.Answers)
            {
                current = _mapper.Map<AnswerTemplate>(answer);
                current.IdQuestion = questionId;

                if (isExist.Type == QuestionType.NumericalOptions)
                {
                    var isNumeric = int.TryParse(current.AnswerText, out var answerNum);
                    if (!isNumeric)
                    {
                        throw new InvalidOperationException("Answer must be numeric!");
                    }
                }

                var id = _answerRepository.Insert(current);
                current.Id = id;
            }

            var allAnswers = _answerRepository.GetAllByQuestionId(questionId);
            return _mapper.Map<ICollection<AnswerListDto>>(allAnswers);
        }

        public ICollection<AnswerListDto> CreateAnswerTemplates(int formId, int moduleId, int questionId, AddListAnswers model)
        {
            var isExistFormModule = _formModuleRepository.CheckIfFormTemplateContainsModuleId(formId, moduleId);
            if (isExistFormModule == null)
            {
                throw new Exception("Invalid form or module.");
            }

            var allAnswerTemplates = CreateAnswer(questionId, model);

            return allAnswerTemplates;
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
        }
    }
}
