using Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class QuestionRepository : IQuestionRepository, IGenericRepository<Question>
    {
        private readonly IConfiguration _configuration;

        public QuestionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
        
        //public void CreateNewQuestion(Question model)
        //{ 
        //    try
        //    {
        //        IDbConnection dbConnection = Connection;
        //        var query = @"INSERT INTO QuestionTemplate 
        //                        VALUES (@Name, @Date, @Type, @IsReusable)";
        //        dbConnection.Execute(query, new { model.Name, Date = DateTime.UtcNow, model.Type, model.IsReusable});
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public Question GetQuestionById(int questionId)
        {
            try
            {
                IDbConnection dbConnection = Connection;
                var query = @"SELECT * FROM QuestionTemplate
                                WHERE Id = @Id";
                return dbConnection.QueryFirstOrDefault<Question>(query, new { Id = questionId });
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteQuestion(int questionId)
        {
            try
            {
                IDbConnection dbConnection = Connection;
                var query = @"DELETE FROM AnswerTemplate
                                WHERE IdQuestion = 1
                                DELETE FROM QuestionTemplate";
                dbConnection.Execute(query, new { Id = questionId });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateCurrentQuestion(int id, Question model)
        {
            try
            {
                IDbConnection dbConnection = Connection;
                var query = @"UPDATE QuestionTemplate
                                SET [Name] = @Name, [Type] = @Type, IsReusable = @ReusableValue
                                WHERE Id = @Id";
                dbConnection.Execute(query, new { model.Name, model.Type, @ReusableValue = model.IsReusable, id });
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        public List<Question> GetAllQuestionsWithAnswers()  // Return  ListQuestionsDto
        {
            try
            {
                IDbConnection dbConnection = Connection; ;
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





                var questionDictionary = new Dictionary<int, Question>();
                var questions = dbConnection.Query<Question, Answer, Question>(query, (question, answer) =>
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


        public void Create(Question model)
        {
            try
            {
                IDbConnection dbConnection = Connection;
                var query = @"INSERT INTO QuestionTemplate 
                                        VALUES (@Name, @Date, @Type, @IsReusable)";
                dbConnection.Execute(query, new { model.Name, Date = DateTime.UtcNow, model.Type, model.IsReusable });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CreateNewQuestion(Question model)
        {
            throw new NotImplementedException();
        }

    }
}
