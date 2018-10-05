﻿namespace Persistence.Sqlite
{
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using Persistence;

    public class SqliteDbConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection Create(string databaseFileName)
        {
            bool createNewDatabase = !File.Exists(databaseFileName);
            if (createNewDatabase)
            {
                SQLiteConnection.CreateFile(databaseFileName);
            }

            var connection = SetupConnection();

            if (createNewDatabase)
            {
                // todo: Move code away from here
                connection.Open();
                CreateDatabase(connection);
                connection.Close();
            }

            return connection;
        }

        private static SQLiteConnection SetupConnection()
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            return connection;
        }

        // todo: Move code away from here
        private static void CreateDatabase(SQLiteConnection connection)
        {
            string sql = "CREATE TABLE Highscore (Id integer primary key autoincrement, Name varchar(20), Score int)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
