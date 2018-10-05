using System.Collections.Generic;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public interface IScoreRepository
    {
        IEnumerable<Highscore> GetHighscores();

        void AddHighscore(Highscore highscore);
    }
}
