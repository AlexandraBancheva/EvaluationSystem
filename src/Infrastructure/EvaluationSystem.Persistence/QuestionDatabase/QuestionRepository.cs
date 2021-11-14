using Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class QuestionRepository : BaseRepository<QuestionTemplate>, IQuestionRepository
    {
       // private readonly IConfiguration _configuration;

        public QuestionRepository(IConfiguration configuration)
            : base(configuration)
        {
          //  _configuration = configuration;
        }

        //public IDbConnection Connection
        //{
        //    get
        //    {
        //        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //    }
        //}

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

        /// Generic Repository!

        //public void CreateNewQuestion(QuestionTemplate model)
        //{
        //    try
        //    {
        //        using IDbConnection dbConnection = Connection;
        //        var query = @"INSERT INTO QuestionTemplate 
        //                        VALUES (@Name, @Date, @Type, @IsReusable)";
        //        dbConnection.Execute(query, new { model.Name, Date = DateTime.UtcNow, model.Type, model.IsReusable });
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public QuestionTemplate GetQuestionById(int questionId)
        //{
        //    try
        //    {
        //        using IDbConnection dbConnection = Connection;
        //        var query = @"SELECT * FROM QuestionTemplate
        //                        WHERE Id = @Id";
        //        return dbConnection.QueryFirstOrDefault<QuestionTemplate>(query, new { Id = questionId });

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public void DeleteQuestion(int questionId)
        //{
        //    try
        //    {
        //        using IDbConnection dbConnection = Connection;
        //        var query = @"DELETE FROM AnswerTemplate
        //                        WHERE IdQuestion = 1
        //                        DELETE FROM QuestionTemplate";
        //        dbConnection.Execute(query, new { Id = questionId });
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public void UpdateCurrentQuestion(int id, QuestionTemplate model)
        //{
        //    try
        //    {
        //        using IDbConnection dbConnection = Connection;
        //        var query = @"UPDATE QuestionTemplate
        //                        SET [Name] = @Name, [Type] = @Type, IsReusable = @ReusableValue
        //                        WHERE Id = @Id";
        //        dbConnection.Execute(query, new { model.Name, model.Type, @ReusableValue = model.IsReusable, id });
        //    }
        //    catch (Exception ex)
        //    {
        //       throw ex;
        //    }
        //}


    }
}
