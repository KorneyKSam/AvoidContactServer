using AvoidContactServer;
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

var serviceProvider = GetServiceCollection().BuildServiceProvider();
var serverController = serviceProvider.GetService<IServerController>();
var messageLogger = serviceProvider.GetService<IDebugger>();
MessageReceiver.Initialize(serviceProvider.GetService<ISignCommandsExecutor>());

while (true)
{
    messageLogger.Log("Enter command:");
    serverController.EnterCommand(Console.ReadLine());
}

static ServiceCollection GetServiceCollection()
{
    var services = new ServiceCollection();
    services.AddSingleton<IDebugger, Debugger>();
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