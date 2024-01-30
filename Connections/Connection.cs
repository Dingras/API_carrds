using MySql.Data.MySqlClient;

namespace API_carrds.Connections
{
    public class Connection : IDisposable
    {
        private MySqlConnection connectionSQL;
        public Connection()
        {
            MySqlConnectionStringBuilder stringSQL = new MySqlConnectionStringBuilder();
            stringSQL.Server = "localhost";
            stringSQL.Database = "db_carrds";
            stringSQL.UserID = "root";
            stringSQL.Password = "";
            connectionSQL = new MySqlConnection(stringSQL.ConnectionString);
        }
        public void Open()
        {
            connectionSQL.Open();
        }
        public void Close()
        {
            connectionSQL.Close();
        }
        public MySqlConnection Connect()
        {
            return connectionSQL;
        }
        public void Dispose()
        {
            Close();
            connectionSQL.Dispose();
        }
    }
}
