using AvoidContactServer.Database;
using AvoidContactServer.Database.Interfaces;
using AvoidContactServer.Database.Networking;
using AvoidContactServer.Database.Repositories;
using AvoidContactServer.Networking;
using AvoidContactServer.Networking.Interfaces;
using AvoidContactServer.Networking.Sign;
using AdvancedDebugger;
using Microsoft.Extensions.DependencyInjection;
using Riptide;
using AvoidContactServer.Debugging;

namespace AvoidContactServer
{
    class Program()
    {
        private static ServiceProvider m_ServiceProvider;
        private static IServerController m_ServerController;

        public static void Main(string[] args)
        {
            DebuggerInitializer.Initialize();
            m_ServiceProvider = GetServiceCollection().BuildServiceProvider();
            m_ServerController = m_ServiceProvider.GetService<IServerController>();
            MessageReceiver.Initialize(m_ServiceProvider.GetService<ISignCommandsExecutor>());
            StartEnterCommands();
        }

        private static void StartEnterCommands()
        {
            while (true)
            {
                Debugger.Log("Enter command:", DebuggerLog.InfoDebug);
                m_ServerController.EnterCommand(Console.ReadLine());
            }
        }

        private static ServiceCollection GetServiceCollection()
        {
            var services = new ServiceCollection();
            services.AddSingleton<SignsInfo>();
            services.AddSingleton<IDBConnector, ACDBConnector>();
            services.AddSingleton<ISignDataGetter, ACDBSignRepository>();
            services.AddSingleton<ISignDataSetter, ACDBSignRepository>();
            services.AddSingleton<IUserSignValidator, SignValidator>();
            services.AddSingleton<Server>();
            services.AddSingleton<IServerController, ServerController>();
            services.AddSingleton<MessageSender>();
            services.AddSingleton<ISignCommandsExecutor, SignCommandsExecutor>();
            return services;
        }
    }
}