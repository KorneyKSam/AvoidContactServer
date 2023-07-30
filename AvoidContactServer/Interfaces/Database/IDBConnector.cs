using System.Data.SqlClient;

namespace AvoidContactServer.Interfaces.Database
{
    public interface IDBConnector
    {
        public SqlConnection GetSqlConnection();
    }
}