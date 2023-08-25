namespace AvoidContactServer.Database
{
    public static class ACDBSQLCommands
    {
        public const string InsertIntoSigns = "INSERT INTO [Signs] (Login, Password, Email, RegistrationDate) VALUES (@Login, @Password, @Email, @RegistrationDate)";
        public const string SelectFromSignsByLogin = "SELECT * FROM [Signs] WHERE Login LIKE @Login";
        public const string SelectEmail = "SELECT Email FROM [Signs] WHERE Email LIKE @Email";
        public const string SelectLogin = "SELECT Login FROM [Signs] WHERE Login LIKE @Login";
    }
}