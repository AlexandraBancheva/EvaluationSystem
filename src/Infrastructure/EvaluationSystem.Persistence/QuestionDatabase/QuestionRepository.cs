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

        public List<QuestionTemplate> GetAllQuestionsWithAnswers()  // Return  ListQuestionsDto
        {
            try
            {
                using IDbConnection dbConnection = Connection; ;
                // SELECT qt.[Name], [at].AnswerText
                // FROM AnswerTemplate AS[at]
                // RIGHT JOIN QuestionTemplate AS qt ON qt.[Id] = [at].QuestionId
                // GROUP BY qt.[Name], [at].[AnswerText]
                // ORDER BY qt.[Name] ASC

                //var query = @"SELECT qt.Id AS IdQuestion, qt.[Name], [at].Id AS IdAnswer, [at].AnswerText
                //                FROM QuestionTemplate AS qt
                //                LEFT JOIN AnswerTemplate AS [at] ON qt.Id = [at].QuestionId";

                //var query = @"SELECT q.Id AS IdQuestion, q.[Name], a.Id AS IdAnswer, a.AnswerText
                //                FROM QuestionTemplate AS q
                //                LEFT JOIN AnswerTemplate AS a ON q.Id = a.IdQuestion";

                var query = @"SELECT *
                                FROM QuestionTemplate AS q
                                LEFT JOIN AnswerTemplate AS a ON q.Id = a.IdQuestion";
                //var results = dbConnection.Query<ListQuestionsDto>(query);
                //return results.Distinct().ToList();





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
