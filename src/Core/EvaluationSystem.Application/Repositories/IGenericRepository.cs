using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationSystem.Application.Repositories
{
    public interface IGenericRepository<T> 
        where T : class
    {
        void Create(T model);
    }
}
