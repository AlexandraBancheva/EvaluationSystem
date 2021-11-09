using Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class QuestionRepository : IQuestionRepository
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
        
        public void CreateNewQuestion(Question model)
        { 
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var query = @"INSERT INTO QuestionTemplate VALUES (@Name, @Date, @Type, @IsReusable)";
                    dbConnection.Execute(query, new { model.Name, Date = DateTime.UtcNow, model.Type, model.IsReusable});
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Question GetQuestionById(int questionId)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var query = @"SELECT * FROM QuestionTemplate
                                    WHERE Id = 1";
                    return dbConnection.QueryFirstOrDefault<Question>(query);
                }
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
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var query = @"DELETE FROM QuestionTemplate
                                  WHERE Id = @Id";
                    dbConnection.Execute(query, new { Id = questionId });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateCurrentQuestion(int id, Question model)
        {
            using (IDbConnection dbConnection = Connection)
            {
                try
                {
                    dbConnection.Open();
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
        }

        public IEnumerable<ListQuestionsDto> GetAllQuestionsWithAnswers()
        {
            using (IDbConnection dbConnection = Connection)
            {
                try
                {
                    dbConnection.Open();
                    // SELECT qt.[Name], [at].AnswerText
                    // FROM AnswerTemplate AS[at]
                    // RIGHT JOIN QuestionTemplate AS qt ON qt.[Id] = [at].QuestionId
                    // GROUP BY qt.[Name], [at].[AnswerText]
                    // ORDER BY qt.[Name] ASC

                    var query = @"SELECT qt.Id AS IdQuestion, qt.[Name], [at].Id AS IdAnswer, [at].AnswerText
                                    FROM QuestionTemplate AS qt
                                    LEFT JOIN AnswerTemplate AS [at] ON qt.Id = [at].QuestionId";
                    var results = dbConnection.Query<ListQuestionsDto>(query);
                    return results;
                                        //var lookup = new Dictionary<int, Question>();
                                        //dbConnection.Query<Question, Answer, Question>(query, (q, a) =>
                                        //{
                                        //    Question question;
                                        //    if (!lookup.TryGetValue(q.Id, out question))
                                        //    {
                                        //        lookup.Add(q.Id, question = q);
                                        //    }

                                        //    if (question.Answers == null)
                                        //    {
                                        //        question.Answers = new List<Answer>();
                                        //    }
                                        //    question.Answers.Add(a);
                                        //    return question;
                                        //}).AsQueryable();

                                        //var res = lookup.Values;
                                        //return res;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
