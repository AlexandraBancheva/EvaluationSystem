using System;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Services
{
    public class AnswersServices : IAnswersServices
    {
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;

        public AnswersServices(IAnswerRepository answerRepository, 
                               IQuestionRepository questionRepository,
                               IMapper mapper)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
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
            var isExistFormModule = _answerRepository.CheckFormIdModuleIdQuestionId(formId, moduleId, questionId);
            if (isExistFormModule == null)
            {
                throw new Exception("Invalid form or module.");
            }

            var allAnswerTemplates = CreateAnswer(questionId, model);

            return allAnswerTemplates;
        }

        public void DeleteAnAnswer(int formId, int moduleId, int questionId, int answerId)
        {
            var isExistFormModuleQuestion = _answerRepository.CheckFormIdModuleIdQuestionIdAnswerId(formId, moduleId, questionId, answerId);
            if (isExistFormModuleQuestion == null)
            {
                throw new Exception("Invalid form or module or question.");
            }

            var curEntity = _answerRepository.GetById(answerId);
            _answerRepository.Delete(curEntity);
        }

        public void DeleteAnswerTemplate(int questionId, int answerId)
        {
            var isExist = _answerRepository.CheckQuestionIdAnswerId(questionId, answerId);
            if (isExist == null)
            {
                throw new Exception("Invalid questionId or answerId.");
            }
            var entity = _answerRepository.GetById(answerId);
            _answerRepository.Delete(entity);
        }

        public ICollection<AnswerListDto> UpdateAnswer(int questionId, int answerId, UpdateAnswerDto model)
        {
            var isExist = _questionRepository.GetById(questionId);
            if (isExist == null)
            {
                throw new InvalidOperationException($"Question with id {questionId} do not exist!");
            }

            var entity = _mapper.Map<AnswerTemplate>(model);
            entity.Id = answerId;
            entity.IdQuestion = questionId;
            _answerRepository.Update(entity);

            var allAnswers = _answerRepository.GetAllByQuestionId(questionId);
            return _mapper.Map<ICollection<AnswerListDto>>(allAnswers);
        }

        public ICollection<AnswerListDto> UpdateAnswerTemplate(int formId, int moduleId, int questionId, int answerId, UpdateAnswerDto model)
        {
            var isExistFormModule = _answerRepository.CheckFormIdModuleIdQuestionId(formId, moduleId, questionId);
            if (isExistFormModule == null)
            {
                throw new Exception("Invalid form or module.");
            }

            var res = UpdateAnswer(questionId, answerId, model);

            return res;
        }
    }
}
