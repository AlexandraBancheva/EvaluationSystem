using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
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

        public void Delete(T entity)
        {
            using var dbConnection = Connection;
            dbConnection.Delete(entity);
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            using var dbConnection = Connection;
            return dbConnection.Get<T>(id);
        }

        public int Insert(T entity)
        {
            using var dbConnection = Connection;
            var id = dbConnection.Insert<T>(entity);
            return (int)id;
        }

        public void Update(T entity)
        {
            using var dbConnection = Connection;
            dbConnection.Update(entity);
        }
    }
}
