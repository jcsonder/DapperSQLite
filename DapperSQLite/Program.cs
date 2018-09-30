using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Dapper;
using DapperSQLite.Model;

namespace DapperSQLite
{
    class Program
    {
        private const string DatabaseFileName = "MyDatabase.sqlite";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello SQLite and Dapper!");

            bool createNewDatabase = !File.Exists(DatabaseFileName);
            if (createNewDatabase)
            {
                SQLiteConnection.CreateFile(DatabaseFileName);
            }

            using (SQLiteConnection connection = SetupConnection())
            {
                if (createNewDatabase)
                {
                    CreateDatabase(connection);
                }

                SeedData(connection);

                ReadHighscores(connection);
            }

            Console.ReadLine();
        }

        private static SQLiteConnection SetupConnection()
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            connection.Open();
            return connection;
        }

        private static void CreateDatabase(SQLiteConnection connection)
        {
            string sql = "create table highscore (id integer primary key autoincrement, name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private static void SeedData(SQLiteConnection connection)
        {
            AddNewHighscore(connection, "Me", 3000);
            AddNewHighscore(connection, "Myself", 6000);
            AddNewHighscore(connection, "And I", 9001);
        }

        private static void AddNewHighscore(SQLiteConnection connection, string name, int score)
        {
            // todo: Prevent SQL injection, use parameters
            var sql = $"insert into highscore (name, score) values ('{name}', {score})";
            var command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private static void ReadHighscores(IDbConnection connection)
        {
            string query = "select id, name, score from highscore";
            IEnumerable<Highscore> highscores = connection.Query<Highscore>(query);

            foreach (Highscore highscore in highscores)
            {
                Console.WriteLine($"{highscore.Id}-{highscore.Name}-{highscore.Score}");
            }
        }
    }
}
