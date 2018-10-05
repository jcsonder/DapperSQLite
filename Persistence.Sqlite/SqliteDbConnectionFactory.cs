namespace Persistence.Sqlite
{
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using Persistence;

    public class SqliteDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _databaseFileName;

        public SqliteDbConnectionFactory(string databaseFileName)
        {
            _databaseFileName = databaseFileName;
        }

        public IDbConnection Create()
        {
            return SetupConnection();
        }

        public void CreateDatabase()
        {
            if (File.Exists(_databaseFileName))
            {
                File.Delete(_databaseFileName);
            }

            SQLiteConnection.CreateFile(_databaseFileName);
            using (var connection = SetupConnection())
            {
                connection.Open();
                CreateDatabase(connection);
                connection.Close();
            }
        }

        private SQLiteConnection SetupConnection()
        {
            return new SQLiteConnection($"Data Source={_databaseFileName};Version=3;");
        }

        // todo: Move code away from here
        private void CreateDatabase(SQLiteConnection connection)
        {
            string sql = "CREATE TABLE Highscore (Id integer primary key autoincrement, Name varchar(20), Score int)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
