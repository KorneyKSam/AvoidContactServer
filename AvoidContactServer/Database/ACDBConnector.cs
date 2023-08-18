using AdvancedDebugger;
using AvoidContactServer.Database.Interfaces;
using AvoidContactServer.Debugging;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AvoidContactServer.Database
{
    public class ACDBConnector : IDBConnector
    {
        private const string ConnectionKey = "ACDB";
        private const string ConnectionOpened = "Подключение установлено!";
        private const string ConnectionNotOpened = "Подключение не установлено!";

        private SqlConnection m_SQLConnection;

        public ACDBConnector()
        {
            m_SQLConnection = CreateSQlConnection();
        }

        public SqlConnection GetSqlConnection()
        {
            return m_SQLConnection;
        }

        private SqlConnection CreateSQlConnection()
        {
            m_SQLConnection = new SqlConnection(GetConnectionString());
            m_SQLConnection.Open();

            if (m_SQLConnection.State == ConnectionState.Open)
            {
                Debugger.Log(ConnectionOpened, DebuggerLog.InfoDebug);
            }
            else
            {
                Debugger.Log($"{ConnectionNotOpened} State: {m_SQLConnection.State}!", DebuggerLog.InfoDebug);
            }

            return m_SQLConnection;
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
        }
    }
}