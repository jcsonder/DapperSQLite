using System.Collections.Generic;
using Domain.Entities;
using Persistence;

namespace Service
{
    public class ScoreService : IScoreService
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ScoreService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<Highscore> GetHighscores()
        {
            using (var uow = new UnitOfWork(_dbConnectionFactory))
            {
                return uow.ScoreRepository.GetHighscores();
            }
        }

        public void AddHighscore(Highscore highscore)
        {
            using (var uow = new UnitOfWork(_dbConnectionFactory))
            {
                uow.ScoreRepository.AddHighscore(new Highscore() { Name = "Fred", Score = 123 });
            }
        }
    }
}
