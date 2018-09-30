using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace DapperSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello SQLite!");

            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            using (SQLiteConnection connection = SetupConnection())
            {
                CreateDatabase(connection);
                SeedData(connection);
                ReadData(connection);
                ReadStructuredData(connection);
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
            string sql = "create table highscores (id integer primary key autoincrement, name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private static void SeedData(SQLiteConnection connection)
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private static void ReadData(SQLiteConnection connection)
        {
            string query = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"Id: {reader["id"]}\tName: {reader["name"]}\tScore: {reader["score"]});
        }

        private static void ReadStructuredData(IDbConnection connection)
        {
            string query = "select * from highscores";
            IEnumerable<Highscore> highscores = connection.Query<Highscore>(query);

            foreach (Highscore highscore in highscores)
            {
                Console.WriteLine($"{highscore.Id}-{highscore.Name}-{highscore.Score}");
            }
        }

        public class Highscore
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
        }
    }
}
