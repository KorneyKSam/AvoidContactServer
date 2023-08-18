using AdvancedDebugger;
using AvoidContactServer.Debugging;
using AvoidContactServer.Networking.Interfaces;
using AvoidContactServer.Networking.Sign;
using Riptide;
using Riptide.Utils;

namespace AvoidContactServer.Networking
{
    public class ServerController : IServerController
    {
        private Dictionary<string, Action> m_ServerCoommands;

        private const string ServerStartedMessage = "An attempt to start the server for the second time has been noticed!";
        private const string ServerCommandsMessage = "Server command list: ";
        private const string InvalidCommandMessage = "Invalid command: ";

        private Server m_Server;
        private ISignCommandsExecutor m_SignCommandsExecutor;

        private ushort m_Port;
        private ushort m_MaxClientCount;

        public ServerController(Server server, ISignCommandsExecutor signCommandsExecutor)
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, false);
            m_Server = server;
            m_SignCommandsExecutor = signCommandsExecutor;
            m_ServerCoommands = GetServerCommands();

            ShowServerCommandsMessage();

            m_Port = 7777;
            m_MaxClientCount = 10;
        }

        public void EnterCommand(string? command)
        {
            if (!string.IsNullOrEmpty(command))
            {
                if (m_ServerCoommands.ContainsKey(command))
                {
                    m_ServerCoommands[command].Invoke();
                }
                else
                {
                    Debugger.Log(InvalidCommandMessage + command, DebuggerLog.InfoWarning);
                }
            }
        }

        private Dictionary<string, Action> GetServerCommands()
        {
            return new Dictionary<string, Action>()
            {
                { "Start", Start },
                { "Stop", Stop },
            };
        }

        private void ShowServerCommandsMessage()
        {
            Debugger.Log(ServerCommandsMessage, DebuggerLog.InfoDebug);

            foreach (var serverCommand in m_ServerCoommands.Keys)
            {
                Debugger.Log(serverCommand, DebuggerLog.InfoDebug);
            }
        }

        private void Start()
        {
            if (!m_Server.IsRunning)
            {
                AddServerListeners();
                m_Server.Start(m_Port, m_MaxClientCount);
                FixedUpdate();
            }
            else
            {
                Debugger.Log(ServerStartedMessage, DebuggerLog.InfoDebug);
            }
        }

        private void Stop()
        {
            if (m_Server.IsRunning)
            {
                m_Server.Stop();
                RemoveServerListeners();
            }
        }

        private void OnClientConnectedHandler(object? sender, ServerConnectedEventArgs e)
        {
            Debugger.Log($"New Client, ID: {e.Client.Id}", DebuggerLog.InfoDebug);
        }

        private void OnClientDisconnectedHandler(object? sender, ServerDisconnectedEventArgs e)
        {
            m_SignCommandsExecutor.UnlinkPlayerIDAndToken(e.Client.Id);
        }

        private async void FixedUpdate()
        {
            while (m_Server.IsRunning)
            {
                m_Server.Update();

                await Task.Delay(20);
            }
        }

        private void AddServerListeners()
        {
            RemoveServerListeners();
            m_Server.ClientConnected += OnClientConnectedHandler;
            m_Server.ClientDisconnected += OnClientDisconnectedHandler;
        }

        private void RemoveServerListeners()
        {
            m_Server.ClientConnected -= OnClientConnectedHandler;
            m_Server.ClientDisconnected -= OnClientDisconnectedHandler;
        }
    }
}