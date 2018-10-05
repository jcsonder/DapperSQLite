using Domain.Entities;
using Persistence.Sqlite;
using Service;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dbFileName = "Scores.sqlite";

            Console.WriteLine("Hello World!");

            var dbConnectionFactor = new SqliteDbConnectionFactory(dbFileName);
            dbConnectionFactor.CreateDatabase();

            ScoreService scoreService = new ScoreService(dbConnectionFactor);
            scoreService.AddHighscore(new Highscore() { Name = "Fred", Score = 123 });

            var highscores = scoreService.GetHighscores();
            foreach (var highscore in highscores)
            {
                Console.WriteLine(highscore);
            }

            Console.ReadLine();
        }
    }
}
