using AdvancedDebugger;

namespace AvoidContactServer.Debugging
{
    public class DebuggerInitializer
    {
        private const string DateTimeFormat = "dd-MM-yyyy HH:mm:ss.ffff";

        public static void Initialize()
        {
            Debugger.EnableLogWriting = true;
            Debugger.Initialize(GetDebuggerLogTypes(), Console.WriteLine, GetLogPath(), DateTimeFormat);
        }

        private static List<DebuggerLogType> GetDebuggerLogTypes()
        {
            return new List<DebuggerLogType>()
            {
                new DebuggerLogType(DebuggerLog.InfoDebug, Console.WriteLine, isLoggedToFile : false, showSystemInfo: false),
                new DebuggerLogType(DebuggerLog.Debug, Console.WriteLine, isLoggedToFile : true),
                new DebuggerLogType(DebuggerLog.InfoWarning, Console.WriteLine, isLoggedToFile : false, showSystemInfo : false),
                new DebuggerLogType(DebuggerLog.Warning, Console.WriteLine, isLoggedToFile : true),
                new DebuggerLogType(DebuggerLog.Error, Console.WriteLine, isLoggedToFile : true),
            };
        }

        private static string GetLogPath()
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}/Logs/Logs.txt";
        }
    }
}
