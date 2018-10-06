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
            // todo: SQL commands&queries are DB specific -> Move to a DB specific namespace.
            // TSQL:   "INSERT INTO Highscore (Name, Score) VALUES (@Name, @Score); SELECT SCOPE_IDENTITY()",
            // SQLite: "INSERT INTO Highscore (Name, Score) VALUES (@Name, @Score); SELECT last_insert_rowid()"

            highscore.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO Highscore (Name, Score) VALUES (@Name, @Score); SELECT last_insert_rowid()",
                param: new { Name = highscore.Name, Score = highscore.Score },
            transaction: Transaction);
        }
    }
}
