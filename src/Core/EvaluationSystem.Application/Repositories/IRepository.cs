using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IRepository<T> 
    {
        List<T> GetAll();

        T Get(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
