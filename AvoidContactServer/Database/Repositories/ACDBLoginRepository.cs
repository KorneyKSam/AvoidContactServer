using AvoidContactServer.Database.Interfaces;
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
            using var sqlCommand = GetSqlCommand(ACDBSQLCommands.InsertIntoLogin);
            sqlCommand.Parameters.AddWithValue("Login", signUpModel.Login);
            sqlCommand.Parameters.AddWithValue("Password", signUpModel.Password);
            sqlCommand.Parameters.AddWithValue("Email", signUpModel.Email);
            sqlCommand.Parameters.AddWithValue("RegistrationDate", DateTime.Now);

            Console.WriteLine(sqlCommand.ExecuteNonQuery().ToString());
        }

        public SignedPlayerModel TryToGetSignedPlayerByLogin(string login)
        {
            using var sqlCommand = GetSqlCommand(ACDBSQLCommands.SelectFromLogin);
            sqlCommand.Parameters.AddWithValue("Login", login);
            using var sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                var signedPlayerModel = new SignedPlayerModel()
                {
                    Login = Convert.ToString(sqlDataReader["Login"]),
                    Password = Convert.ToString(sqlDataReader["Password"]),
                    Email = Convert.ToString(sqlDataReader["Email"])
                };
                return signedPlayerModel;
            }
            else
            {
                return new SignedPlayerModel();
            }
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