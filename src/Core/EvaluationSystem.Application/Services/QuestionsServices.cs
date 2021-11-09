using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Application.Services
{
    public class QuestionsServices : IQuestionsServices
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionsServices(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public QuestionDetailDto GetQuestionById(int questionId)
        {
            var currentEntity =  _questionRepository.GetQuestionById(questionId);

            if (currentEntity == null)
            {
                return null;
            }

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto CreateNewQuestion(CreateQuestionDto model)
        {
            var currentEntity = _mapper.Map<Question>(model);
            _questionRepository.CreateNewQuestion(currentEntity);

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model)
        {
            var isExist = _questionRepository.GetQuestionById(questionId);

            if (isExist == null)
            {
                return null;
            }

            var current = _mapper.Map<Question>(model);
            _questionRepository.UpdateCurrentQuestion(questionId, current);

            return _mapper.Map<QuestionDetailDto>(current);
        }

        public void DeleteQuestion(int questionId)
        {
            var entity = _questionRepository.GetQuestionById(questionId);
            if (entity == null)
            {
                throw new InvalidOperationException($"Question with {questionId} don't exist!");
            }

            _questionRepository.DeleteQuestion(questionId);
        }

        public IEnumerable<ListQuestionsAnswersDto> GetAllQuestionsWithTheirAnswers()
        {
            var questions = _questionRepository.GetAllQuestionsWithAnswers();
            //var allQuestions = _mapper.Map<IEnumerable<ListQuestionsDto>>(questions);

            //var results = allQuestions.GroupBy(i => i.QuestionName)
            //    .Select(q => new ListQuestionsAnswersDto
            //    {
            //        QuestionName = q.Key,
            //        Answers = questions.Where(a => a.QuestionName == q.Key).Select(y => y.AnswerText).ToList()
            //    })
            //    .ToList();

            var results = questions
                .Select(q => new ListQuestionsAnswersDto
                {
                    IdQuestion = q.IdQuestion,
                    QuestionName = q.Name,
                    Answers = questions
                                .Where(i => i.IdQuestion == q.IdQuestion ) //&& i.IdAnswer == q.IdAnswer)
                                .Select(y => y.AnswerText)
                                .ToList()
                });

            return results;
        }
    }
}
