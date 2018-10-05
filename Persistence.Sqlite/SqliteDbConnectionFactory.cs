namespace Persistence.Sqlite
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

            return SetupConnection();
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
    }
}
