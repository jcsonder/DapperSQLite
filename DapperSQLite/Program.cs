using System;
using Persistence;
using Persistence.Sqlite;

namespace DapperSQLite
{
    class Program
    {
        private const string DatabaseFileName = "MyDatabase.sqlite";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello SQLite and Dapper!");

            using (var uow = new UnitOfWork(new SqliteDbConnectionFactory(), "DatabaseFileName"))
            {
                var x = uow.ScoreRepository.GetHighscores();
            }

            //SeedData(connection);
            //ReadHighscores(connection);

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
