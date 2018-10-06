using Domain.Entities;
using Persistence.Sqlite;
using Service;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dbFileName = "Scores.sqlite";

            Console.WriteLine("Hello World!");

            // todo: Use dependency injection
            var dbConnectionFactor = new SqliteDbConnectionFactory(dbFileName);
            if (!File.Exists(dbFileName))
            {
                dbConnectionFactor.CreateDatabase();
            }

            ScoreService scoreService = new ScoreService(dbConnectionFactor);
            scoreService.AddHighscore(new Highscore() { Name = "Fred", Score = 123 });
            scoreService.AddHighscore(new Highscore() { Name = "Frank", Score = 999 });

            var highscores = scoreService.GetHighscores();
            foreach (var highscore in highscores)
            {
                Console.WriteLine(highscore);
            }

            Console.ReadLine();
        }
    }
}
