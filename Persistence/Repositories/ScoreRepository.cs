using System.Collections.Generic;
using System.Data;
using Persistence.Entities;
using Dapper;
using System.Linq;

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
                "SELECT id, name, score " +
                "FROM highscore", Transaction)
                .ToList();
        }

        //public void AddHighscore(Highscore highscore)
        //{
        //    // todo: Prevent SQL injection, use parameters
        //    var sql = $"insert into highscore (name, score) values ('{name}', {score})";
        //    var command = new SQLiteCommand(sql, connection);
        //    command.ExecuteNonQuery();
        //}
    }
}
