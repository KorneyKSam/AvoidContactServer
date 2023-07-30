using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Interfaces;
using AvoidContactServer.Interfaces.Networking;
using Riptide;
using Riptide.Utils;

namespace AvoidContactServer.Networking
{
    public class ServerController : IServerController
    {
        private const string ServerStartedMessage = "An attempt to start the server for the second time has been noticed!";

        private IMessageLogger m_MessageLogger;
        private Server m_Server;
        private SignedPlayers m_SignedPlayers;

        public ServerController(IMessageLogger messageLogger, Server server, SignedPlayers signedPlayers)
        {
            m_MessageLogger = messageLogger;
            RiptideLogger.Initialize(m_MessageLogger.Log, m_MessageLogger.LogInfo, m_MessageLogger.LogWarning, m_MessageLogger.LogError, false);
            m_Server = server;
            m_SignedPlayers = signedPlayers;
        }

        public void Start(ushort port, ushort maxClientCount)
        {
            if (!m_Server.IsRunning)
            {
                AddServerListeners();
                m_Server.Start(port, maxClientCount);
                FixedUpdate();
            }
            else
            {
                m_MessageLogger.LogWarning(ServerStartedMessage);
            }
        }

        public void Stop()
        {
            if (m_Server.IsRunning)
            {
                m_Server.Stop();
                RemoveServerListeners();
            }
        }

        private void OnClientDisconnectedHandler(object? sender, ServerDisconnectedEventArgs e)
        {
            var disconnectedPlayer = m_SignedPlayers.List.FirstOrDefault(p => p.PlayerId == e.Client.Id);
            if (disconnectedPlayer != null)
            {
                m_SignedPlayers.List.Remove(disconnectedPlayer);
            }
        }

        private void AddServerListeners()
        {
            RemoveServerListeners();
            m_Server.ClientDisconnected += OnClientDisconnectedHandler;
        }

        private void RemoveServerListeners()
        {
            m_Server.ClientDisconnected -= OnClientDisconnectedHandler;
        }

        private async void FixedUpdate()
        {
            while (m_Server.IsRunning)
            {
                m_Server.Update();

                await Task.Delay(20);
            }
        }
    }
}