namespace AvoidContactServer.Database
{
    public static class ACDBSQLCommands
    {
        public const string InsertIntoLogin = "INSERT INTO [Login] (Login, Password, Email, RegistrationDate) VALUES (@Login, @Password, @Email, @RegistrationDate)";
        public const string SelectFromLogin = "SELECT * FROM [Login] WHERE Login LIKE @Login";
    }
}