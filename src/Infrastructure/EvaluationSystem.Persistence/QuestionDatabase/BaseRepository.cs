using EvaluationSystem.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
