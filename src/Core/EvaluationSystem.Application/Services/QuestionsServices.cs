using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

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
            var currentEntity =  _questionRepository.GetById(questionId);

            if (currentEntity == null)
            {
                return null;
            }

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto CreateNewQuestion(CreateQuestionDto model)
        {
            var currentEntity = _mapper.Map<QuestionTemplate>(model);
            currentEntity.DateOfCreation = DateTime.UtcNow;
            _questionRepository.Insert(currentEntity);

            return _mapper.Map<QuestionDetailDto>(currentEntity);
        }

        public QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model)
        {
            var isExist = _questionRepository.GetById(questionId);

            if (isExist == null)
            {
                return null;
            }

            var current = _mapper.Map<QuestionTemplate>(model);
            current.Id = questionId;
            current.DateOfCreation = DateTime.UtcNow;
            _questionRepository.Update(current);

            return _mapper.Map<QuestionDetailDto>(current);
        }

        public void DeleteQuestion(int questionId)
        {
            var entity = _questionRepository.GetById(questionId);
            _questionRepository.Delete(entity);
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

            //var results = questions
            //    .Select(q => new ListQuestionsAnswersDto
            //    {
            //        IdQuestion = q.Id,
            //        QuestionName = q.Name,
            //        Answers = questions
            //                  //  .Where(i => i.IdQuestion == q.IdQuestion)                                                      //&& i.IdAnswer == q.IdAnswer)
            //                                                                                                                   //.Select(y => y.AnswerText)
            //                    .Select(a => new AnswerListDto1
            //                    //{
            //                    //    IdAnswer = a.Answers.Select(y => y.Id),
            //                    //    AnswerText = a.AnswerText
            //                    //})
            //                    .ToList()
            //    })
            //    .ToList();


            // // Correct so so!!! // //
            //var questionsAll = questions
            //                .Select(q => new ListQuestionsAnswersDto
            //                {
            //                    IdQuestion = q.Id,
            //                    QuestionName = q.Name,
            //                    Answers = q.Answers.Where(a => q.Id == a.IdQuestion)   // Null exception
            //                                                          .Select(y => new AnswerListDto1
            //                                                          {
            //                                                              IdAnswer = y.Id,
            //                                                              AnswerText = y.AnswerText
            //                                                          }).ToList() //?? new List<AnswerListDto1>()
            //                }).ToList();

            var questionsAll = questions
                .Select(q => new ListQuestionsAnswersDto
                {
                    IdQuestion = q.Id,
                    QuestionName = q.Name,
                    Answers = /* q.Answers.Where(a => q.Id == a.Id).Select(y => new AnswerListDto1 { IdAnswer = y.Id, AnswerText = y.AnswerText}).ToList() ?? */ new List<AnswerListDto1>()
                }); ;

            foreach (var item in questions)
            {
                var itemId = item.Id;
    //            item.Answers.Add(new AnswerListDto1(quest));
            }


            return questionsAll;
        }
    }
}
