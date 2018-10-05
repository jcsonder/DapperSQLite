using System.Collections.Generic;
using Domain.Entities;

namespace Service
{
    public interface IScoreService
    {
        IEnumerable<Highscore> GetHighscores();

        void AddHighscore(Highscore highscore);
    }
}
