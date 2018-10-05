using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Entities;
using Dapper;

namespace Persistence.Repositories
{
    internal class ScoreRepository : Repository, IScoreRepository
    {
        public ScoreRepository(IDbTransaction transaction)
            : base(transaction)
        {
        } 

        public IEnumerable<Highscore> GetHighscores()
        {
            return Connection.Query<Highscore>(
                "SELECT Id, Name, Score " +
                "FROM Highscore", Transaction)
                .ToList();
        }

        public void AddHighscore(Highscore highscore)
        {
            highscore.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO Highscore(Name, Score) VALUES (@Name, @Score)",
                //"INSERT INTO highscore(Name, Score) VALUES(@Name, @Score); SELECT SCOPE_IDENTITY()",
                param: new { Name = highscore.Name, Score = highscore.Score },
            transaction: Transaction);
        }
    }
}
