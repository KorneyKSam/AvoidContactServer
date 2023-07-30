namespace AvoidContactServer.Interfaces
{
    public interface IMessageLogger
    {
        public void Log(string message);
        public void LogInfo(string message);
        public void LogError(string message);
        public void LogWarning(string message);
    }
}
