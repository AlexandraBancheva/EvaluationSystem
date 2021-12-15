﻿using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        List<T> GetAll();

        T GetById(int id);

        int Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
