using System.Data.SqlClient;

namespace AvoidContactServer.Database.Interfaces
{
    public interface IDBConnector
    {
        public SqlConnection GetSqlConnection();
    }
}