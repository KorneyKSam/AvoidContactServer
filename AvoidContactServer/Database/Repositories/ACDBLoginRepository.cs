using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Interfaces.Database;
using System.Data;
using System.Data.SqlClient;

namespace AvoidContactServer.Database.Repositories
{
    public class ACDBLoginRepository : ILoginRepository
    {
        private SqlConnection m_SqlConnection;

        public ACDBLoginRepository(IDBConnector dBConnector)
        {
            m_SqlConnection = dBConnector.GetSqlConnection();
        }

        public void AddPlayer(SignedPlayerModel signUpModel)
        {
            using var sqlCommand = GetSqlCommand(ACDBSQLCommands.AddLogin);
            sqlCommand.Parameters.AddWithValue("Login", signUpModel.Login);
            sqlCommand.Parameters.AddWithValue("Password", signUpModel.Password);
            sqlCommand.Parameters.AddWithValue("Email", signUpModel.Email);
            sqlCommand.Parameters.AddWithValue("RegistrationDate", DateTime.Now);

            Console.WriteLine(sqlCommand.ExecuteNonQuery().ToString());
        }

        public SignedPlayerModel TryToGetSignedPlayerByLogin(string login)
        {
            using var sqlCommand = GetSqlCommand(ACDBSQLCommands.GetLogin);
            sqlCommand.Parameters.AddWithValue("Login", login);
            var dataAdapter = new SqlDataAdapter(sqlCommand);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            var datasetTable = dataSet.Tables[0];

            return GetSignedPlayerFromTable();
        }

        public SignedPlayerModel TryToGetSignedPlayerByEmail(string email)
        {
            return GetSignedPlayerFromTable();
        }

        private SqlCommand GetSqlCommand(string command)
        {
            return new SqlCommand(command, m_SqlConnection);
        }

        private SignedPlayerModel GetSignedPlayerFromTable(/*TODO: parse table values*/)
        {
            return new SignedPlayerModel();
        }
    }
}