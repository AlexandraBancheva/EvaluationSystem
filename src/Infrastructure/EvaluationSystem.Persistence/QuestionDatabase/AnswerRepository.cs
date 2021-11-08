using Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly FakeDatabase fakeDatabase;

        protected readonly IConfiguration _configuration;

        public AnswerRepository(IConfiguration configuration, FakeDatabase fakeDatabase)
        {
            this.fakeDatabase = fakeDatabase;
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public void AddNewAnswer(int questionId, Answer model)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var query = @"INSERT INTO AnswerTemplate 
                                    VALUES (@IsDefault, @Position, @AnswerText, @QuestionId)";
                    dbConnection.Execute(query, new { IsDefault = model.IsDefault, Position = model.Position, AnswerText = model.AnswerText, QuestionId = questionId});
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteAnAnswer(int answerId)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var query = @"DELETE FROM AnswerTemplate
                                    WHERE Id = @Id";
                    dbConnection.Execute(query, new { Id = answerId });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Not here!!!! api/questions/{questionId}/answers!!!!
        public IEnumerable<Answer> GetAllAnswersByQuestionId(int questionId)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var query = @"SELECT qt.[Name], [at].AnswerText
                                    FROM QuestionTemplate AS qt
                                    JOIN AnswerTemplate AS [at] ON qt.Id = [at].QuestionId
                                    WHERE qt.Id = @QuestionId";
                  return dbConnection.Query<Answer>(query, new { QuestionId = questionId});
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //return fakeDatabase.Answers.Where(a => a.QuestionId == questionId);
        }
    }
}
