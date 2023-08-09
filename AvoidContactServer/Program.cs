using AvoidContactServer.Database;
using AvoidContactServer.Database.Interfaces;
using AvoidContactServer.Database.Networking;
using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Database.Repositories;
using AvoidContactServer.Debugger.Interfaces;
using AvoidContactServer.Logger;
using AvoidContactServer.Networking;
using AvoidContactServer.Networking.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Riptide;

namespace AvoidContactServer
{
    class Program()
    {
        private static ServiceProvider m_ServiceProvider;
        private static IServerController m_ServerController;
        private static IDebugger m_Debugger;

        public static void Main(string[] args)
        {
            m_ServiceProvider = GetServiceCollection().BuildServiceProvider();
            m_ServerController = m_ServiceProvider.GetService<IServerController>();
            m_Debugger = m_ServiceProvider.GetService<IDebugger>();
            MessageReceiver.Initialize(m_ServiceProvider.GetService<ISignCommandsExecutor>());

            StartEnterCommands();
        }

        private static void StartEnterCommands()
        {
            while (true)
            {
                m_Debugger.Log("Enter command:");
                m_ServerController.EnterCommand(Console.ReadLine());
            }
        }

        private static ServiceCollection GetServiceCollection()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDebugger, AdvancedDebugger>();
            services.AddSingleton<SignedPlayers>();
            services.AddSingleton<IDBConnector, ACDBConnector>();
            services.AddSingleton<ILoginRepository, ACDBLoginRepository>();
            services.AddSingleton<IUserSignValidator, ACDBSignValidator>();
            services.AddSingleton<Server>();
            services.AddSingleton<IServerController, ServerController>();
            services.AddSingleton<MessageSender>();
            services.AddSingleton<ISignCommandsExecutor, SignCommandsExecutor>();
            return services;
        }
    }
}