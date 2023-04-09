namespace AvoidContactServer.Database
{
    internal static class SQLCommands
    {
        internal const string SignUp = "INSERT INTO [Login] (Login, Password, Email, RegistrationDate) VALUES (@Login, @Password, @Email, @RegistrationDate)";
    }
}