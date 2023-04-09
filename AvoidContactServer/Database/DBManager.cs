using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AvoidContactServer.Database
{
    internal class DBManager
    {
        private const string ConnectionKey = "ACDB";
        private SqlConnection? m_SQLConnection;

        public void SetDBConnection()
        {
            m_SQLConnection = new SqlConnection(GetConnectionString());
            m_SQLConnection.Open();

            if (m_SQLConnection.State == ConnectionState.Open)
            {
                Console.WriteLine("Подключение установлено");
            }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
        }
    }
}