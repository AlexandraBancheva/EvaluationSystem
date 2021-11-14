using Dapper;
using EvaluationSystem.Application.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        public void Delete(int id)
        {
            using var dbConnection = Connection;
            dbConnection.Delete(id);
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
            throw new NotImplementedException();
        }
    }
}
