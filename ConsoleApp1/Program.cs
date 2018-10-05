using Persistence;
using Persistence.Entities;
using Persistence.Sqlite;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var connectionFactory = new SqliteDbConnectionFactory();
            using (var uow = new UnitOfWork(connectionFactory, "DatabaseFileName"))
            {
                uow.ScoreRepository.AddHighscore(new Highscore() { Name = "Fred", Score = 123 });

                var highscores = uow.ScoreRepository.GetHighscores();
                foreach(var highscore in highscores)
                {
                    Console.WriteLine(highscore);
                }
            }

            Console.ReadLine();
        }
    }
}
