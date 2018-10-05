using Persistence.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IScoreRepository _scoreRepository;
        private bool _disposed;

        public UnitOfWork(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.Create();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IScoreRepository ScoreRepository
        {
            get { return _scoreRepository ?? (_scoreRepository = new ScoreRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _scoreRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
