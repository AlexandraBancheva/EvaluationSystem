﻿using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }

        public IDbConnection Connection
        {
            get 
            { 
                return _connection; 
            }
        }

        public IDbTransaction Transaction { 
            get 
            {
                return _transaction; 
            }
        }

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }
    }
}
