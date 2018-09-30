using System;
using System.Data.SQLite;

namespace DapperSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello SQLite!");

            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            using (SQLiteConnection m_dbConnection = SetupConnection())
            {
                CreateDatabase(m_dbConnection);

                SeedData(m_dbConnection);

                ReadData(m_dbConnection);
            }

            Console.ReadLine();
        }

        private static SQLiteConnection SetupConnection()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
            return m_dbConnection;
        }

        private static void SeedData(SQLiteConnection m_dbConnection)
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        private static void CreateDatabase(SQLiteConnection m_dbConnection)
        {
            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        private static void ReadData(SQLiteConnection m_dbConnection)
        {
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
        }
    }
}
