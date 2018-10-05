using Persistence;
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
                var x = uow.ScoreRepository.GetHighscores();
            }

            Console.ReadLine();
        }

        //private static void SeedData(SQLiteConnection connection)
        //{
        //    AddNewHighscore(connection, "Me", 3000);
        //    AddNewHighscore(connection, "Myself", 6000);
        //    AddNewHighscore(connection, "And I", 9001);
        //}
    }
}
