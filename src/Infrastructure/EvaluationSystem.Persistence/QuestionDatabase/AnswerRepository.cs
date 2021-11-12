using Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class AnswerRepository : IAnswerRepository
    {
        protected readonly IConfiguration _configuration;

        public AnswerRepository(IConfiguration configuration)
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
    }
}
