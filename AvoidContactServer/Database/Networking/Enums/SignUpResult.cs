namespace AvoidContactServer.Database.Networking.Enums
{
    internal enum SignUpResult : byte
    {
        Success = 0,
        LoginUsed = 1,
        EmailUsed = 2,
        BadPassword = 3,
    }
}