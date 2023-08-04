namespace AvoidContactServer.Debugger.Interfaces
{
    public interface IDebugger
    {
        public void Log(string message);
        public void LogInfo(string message);
        public void LogError(string message);
        public void LogWarning(string message);
    }
}