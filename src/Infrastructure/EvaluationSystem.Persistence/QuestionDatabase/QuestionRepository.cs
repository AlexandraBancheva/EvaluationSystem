using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class QuestionRepository : BaseRepository<QuestionTemplate>, IQuestionRepository
    {

        public QuestionRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void DeleteQuestion(int questionId)
        {
            using var dbConnection = Connection;
            var query = @"DELETE FROM AnswerTemplate
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM ModuleQuestion
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM QuestionTemplate
                            WHERE Id = @QuestionId";
            dbConnection.Execute(query, new { QuestionId = questionId});
        }
        
        public ICollection<QuestionTemplate> GetAllQuestionsWithAnswers()
        {
            try
            {
                using IDbConnection dbConnection = Connection;
                var query = @"SELECT *
                                FROM QuestionTemplate AS q
                                LEFT JOIN AnswerTemplate AS a ON q.Id = a.IdQuestion";
                var questionDictionary = new Dictionary<int, QuestionTemplate>();
                var questions = dbConnection.Query<QuestionTemplate, AnswerTemplate, QuestionTemplate>(query, (question, answer) =>
                {
                    if (!questionDictionary.TryGetValue(question.Id, out var currentQuestion))
                    {
                        currentQuestion = question;
                        questionDictionary.Add(currentQuestion.Id, currentQuestion);
                    }

                    currentQuestion.Answers.Add(answer);
                    return currentQuestion;
                },
                splitOn: "Id")
                    .Distinct()
                    .ToList();

                return questions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
