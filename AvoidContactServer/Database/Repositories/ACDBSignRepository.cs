using AvoidContactCommon.Sign;
using AvoidContactServer.Database.Interfaces;
using System.Data.SqlClient;

namespace AvoidContactServer.Database.Repositories
{
    public class ACDBSignRepository : ISignDataGetter, ISignDataSetter
    {
        private SqlConnection m_SqlConnection;

        public ACDBSignRepository(IDBConnector dBConnector)
        {
            m_SqlConnection = dBConnector.GetSqlConnection();
        }

        public void AddPlayer(SignInfo signInfo)
        {
            using var sqlCommand = GetSqlCommand(ACDBSQLCommands.InsertIntoSigns);
            sqlCommand.Parameters.AddWithValue("Login", signInfo.Login);
            sqlCommand.Parameters.AddWithValue("Password", signInfo.Password);
            sqlCommand.Parameters.AddWithValue("Email", signInfo.Email);
            sqlCommand.Parameters.AddWithValue("RegistrationDate", DateTime.Now);

            Console.WriteLine(sqlCommand.ExecuteNonQuery().ToString());
        }

        public SignInfo TryToGetSignByLogin(string login)
        {
            using var sqlCommand = GetSqlCommand(ACDBSQLCommands.SelectFromSignsByLogin);
            sqlCommand.Parameters.AddWithValue("Login", login);
            using var sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                var signedPlayerModel = new SignInfo()
                {
                    Login = Convert.ToString(sqlDataReader["Login"]),
                    Password = Convert.ToString(sqlDataReader["Password"]),
                    Email = Convert.ToString(sqlDataReader["Email"])
                };
                return signedPlayerModel;
            }
            else
            {
                return new SignInfo();
            }
        }

        public bool CheckEmailUsed(string email)
        {
            return ExecuteExistCommand("Email", email, ACDBSQLCommands.SelectEmail);
        }

        public bool CheckLoginUsed(string login)
        {
            return ExecuteExistCommand("Login", login, ACDBSQLCommands.SelectLogin);
        }

        public bool ExecuteExistCommand(string parametername, string parameterValue, string command)
        {
            using var sqlCommand = GetSqlCommand(command);
            sqlCommand.Parameters.AddWithValue(parametername, parameterValue);
            using var sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                return sqlDataReader[parametername] != null;
            }
            return false;
        }

        private SqlCommand GetSqlCommand(string command)
        {
            return new SqlCommand(command, m_SqlConnection);
        }
    }
}