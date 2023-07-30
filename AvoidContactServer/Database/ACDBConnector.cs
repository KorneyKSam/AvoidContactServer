using AvoidContactServer.Interfaces;
using AvoidContactServer.Interfaces.Database;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AvoidContactServer.Database
{
    public class ACDBConnector : IDBConnector
    {
        public ACDBConnector(IMessageLogger logger)
        {
            m_Logger = logger;
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
                m_Logger.Log(ConnectionOpened);
            }
            else
            {
                m_Logger.LogError($"{ConnectionNotOpened} State: {m_SQLConnection.State}!");
            }

            return m_SQLConnection;
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
        }

        private const string ConnectionKey = "ACDB";
        private const string ConnectionOpened = "Подключение установлено!";
        private const string ConnectionNotOpened = "Подключение не установлено!";

        private SqlConnection m_SQLConnection;
        private IMessageLogger m_Logger;
    }
}