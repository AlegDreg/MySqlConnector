using MySql.Data.MySqlClient;

namespace MySqlConnector
{
    public class DB
    {
        string Server { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Database { get; set; }
        string Port { get; set; }

        MySqlConnection connection;

        public DB(string server, string username, string password, string db, string port = null)
        {
            Server = server;
            Username = username;
            Password = password;
            Database = db;
            Port = port;

            Initialize();
        }

        private void Initialize()
        {
            string l = 
                $"server={Server};" +
                $"username={Username};" +
                $"password={Password};" +
                $"database={Database};" +
                $"convert zero datetime=True";

            if(Port != null)
            {
                l += $";port ={Port};";
            }

            connection = new MySqlConnection(l);
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        ~DB(){
            CloseConnection();
            connection.Dispose();
        }
    }
}
