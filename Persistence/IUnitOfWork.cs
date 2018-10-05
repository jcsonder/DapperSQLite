using Persistence.Repositories;
using System;

namespace Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IScoreRepository ScoreRepository { get; }
        
        void Commit();
    }
}
