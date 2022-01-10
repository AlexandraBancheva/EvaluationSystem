using System.Data;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IDbTransaction Transaction => _unitOfWork.Transaction;

        public IDbConnection Connection => _unitOfWork.Connection;

        public void Delete(T entity)
        {
            Connection.Delete(entity, Transaction);
        }

        public List<T> GetAll()
        {
            return Connection.GetList<T>(null, null, Transaction).ToList();
        }

        public T GetById(int id)
        {
            var res = Connection.Get<T>(id, Transaction);
            return res;
        }

        public int Insert(T entity)
        {
            var id = Connection.Insert<T>(entity, Transaction);
            return (int)id;
        }

        public void Update(T entity)
        {
            Connection.Update(entity, Transaction);
        }
    }
}
