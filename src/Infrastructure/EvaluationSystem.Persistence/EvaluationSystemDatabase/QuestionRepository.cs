﻿using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class QuestionRepository : BaseRepository<QuestionTemplate>, IQuestionRepository
    {
        public QuestionRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteTemplateQuestion(int questionId)
        {
            var query = @"DELETE FROM AnswerTemplate
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM ModuleQuestion
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM QuestionTemplate
                            WHERE Id = @QuestionId";
            Connection.Execute(query, new { QuestionId = questionId}, Transaction);
        }

        public ICollection<QuestionTemplate> GetAllById(int questionId)
        {
            var query = @"SELECT *
                            FROM QuestionTemplate AS q
                            LEFT JOIN AnswerTemplate AS a ON q.Id = a.IdQuestion
                            WHERE q.Id = @QuestionId";

            var queryParameter = new { QuestionId = questionId };

            var questionDictionary = new Dictionary<int, QuestionTemplate>();
            var questions = Connection.Query<QuestionTemplate, AnswerTemplate, QuestionTemplate>(query, (question, answer) =>
            {
                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }

                currentQuestion.Answers.Add(answer);
                return currentQuestion;
            }, queryParameter, Transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return questions;
        }

        public ICollection<QuestionTemplate> GetAllQuestionsWithAnswers()
        {
            var query = @"SELECT *
                            FROM QuestionTemplate AS q
                            LEFT JOIN AnswerTemplate AS a ON q.Id = a.IdQuestion";
            var questionDictionary = new Dictionary<int, QuestionTemplate>();
            var questions = Connection.Query<QuestionTemplate, AnswerTemplate, QuestionTemplate>(query, (question, answer) =>
                {
                    if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                    {
                        currentQuestion = question;
                        questionDictionary.Add(currentQuestion.Id, currentQuestion);
                    }

                    currentQuestion.Answers.Add(answer);
                    return currentQuestion;
            }, Transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return questions;
        }

        public ICollection<QuestionTemplate> GetAllQuestionTemplates()
        {
            var query = @"SELECT *
                    FROM QuestionTemplate AS q
                    LEFT JOIN AnswerTemplate AS a ON q.Id = a.IdQuestion
                    WHERE q.IsReusable = 'true'";

            var questionDictionary = new Dictionary<int, QuestionTemplate>();
            var allQuestionTemplates = Connection.Query<QuestionTemplate, AnswerTemplate, QuestionTemplate>(query, (question, answer) =>
            {
                if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                {
                    currentQuestion = question;
                    questionDictionary.Add(currentQuestion.Id, currentQuestion);
                }

                currentQuestion.Answers.Add(answer);
                return currentQuestion;
            }, Transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return allQuestionTemplates;
        }
    }
}