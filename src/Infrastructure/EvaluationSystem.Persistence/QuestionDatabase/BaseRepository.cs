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
        //private readonly IConfiguration _configuration;
        

        private readonly IUnitOfWork _unitOfWork;

        //public BaseRepository(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IDbTransaction _transaction => _unitOfWork.Transaction;

        public IDbConnection _connection => _unitOfWork.Connection;

        //public IDbConnection Connection
        //{
        //    get
        //    {
        //        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //    }
        //}

        public void Delete(T entity)
        {
            // using var dbConnection = Connection;
            // dbConnection.Delete(entity, _transaction.Connection);
            _connection.Delete(entity, _transaction);
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
           // using var dbConnection = Connection;
            return _connection.Get<T>(id);
        }

        public int Insert(T entity)
        {
           // using var dbConnection = Connection;
            var id = _connection.Insert<T>(entity);
            return (int)id;
        }

        public void Update(T entity)
        {
            // using var dbConnection = Connection;
            _connection.Update(entity);
        }
    }
}
