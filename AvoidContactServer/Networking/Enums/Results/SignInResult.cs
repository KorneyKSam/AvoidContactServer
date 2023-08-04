namespace AvoidContactServer.Networking.Enums.Results
{
    public enum SignInResult : ushort
    {
        Success = 0,
        WrongLoginOrPassword = 1,
        AccountIsOccupied = 2,
    }
}