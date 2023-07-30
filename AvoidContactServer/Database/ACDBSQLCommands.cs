namespace AvoidContactServer.Database
{
    internal static class ACDBSQLCommands
    {
        internal const string AddLogin = "INSERT INTO [Login] (Login, Password, Email, RegistrationDate) VALUES (@Login, @Password, @Email, @RegistrationDate)";
        internal const string GetLogin = "SELECT * FROM [Login] WHERE @Login";
    }
}