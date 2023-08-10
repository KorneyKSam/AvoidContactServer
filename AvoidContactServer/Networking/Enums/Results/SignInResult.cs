namespace AvoidContactServer.Networking.Enums.Results
{
    public enum SignInResult : byte
    {
        Success = 0,
        WrongLoginOrPassword = 1,
        AccountIsOccupied = 2,
    }
}