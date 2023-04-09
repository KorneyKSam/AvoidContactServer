using AvoidContactServer.Database.Networking.Models;
using System.Data.SqlClient;

namespace AvoidContactServer.Database.Repositories
{
    internal class LoginRepository
    {
        private SqlConnection m_SqlConnection;

        public LoginRepository(SqlConnection sqlConnection)
        {
            m_SqlConnection = sqlConnection;
        }

        internal void AddPlayer(SignUpModel signUpModel)
        {
            var sqlCommand = new SqlCommand(SQLCommands.SignUp, m_SqlConnection);

            sqlCommand.Parameters.AddWithValue("Login", signUpModel.Login);
            sqlCommand.Parameters.AddWithValue("Password", signUpModel.Password);
            sqlCommand.Parameters.AddWithValue("Email", signUpModel.Email);
            sqlCommand.Parameters.AddWithValue("RegistrationDate", DateTime.Now);

            Console.WriteLine(sqlCommand.ExecuteNonQuery().ToString());
        }
    }
}