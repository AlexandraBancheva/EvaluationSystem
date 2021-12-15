using System;
using System.Data;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void Begin();

        void Commit();

        void Rollback();
    }
}
