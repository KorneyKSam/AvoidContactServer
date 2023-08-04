using AvoidContactServer.Debugger.Interfaces;

namespace AvoidContactServer.Logger
{
    public class Debugger : IDebugger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        public void LogError(string message)
        {
            Console.WriteLine(message);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine(message);
        }
    }
}
