using AvoidContactServer;
using AvoidContactServer.Database;
using AvoidContactServer.Database.Networking;
using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Database.Repositories;
using AvoidContactServer.Interfaces;
using AvoidContactServer.Interfaces.Database;
using AvoidContactServer.Interfaces.Networking;
using AvoidContactServer.Logger;
using AvoidContactServer.Networking;
using Microsoft.Extensions.DependencyInjection;
using Riptide;

//var dbManager = new DBManager();
//dbManager.SetDBConnection();

var services = new ServiceCollection();
services.AddSingleton<IMessageLogger, Logger>();
services.AddSingleton<SignedPlayers>();
services.AddSingleton<IDBConnector, ACDBConnector>();
services.AddSingleton<ILoginRepository, ACDBLoginRepository>();
services.AddSingleton<IUserSignValidator, ACDBSignValidator>();
services.AddSingleton<Server>();
services.AddSingleton<IServerController, ServerController>();
services.AddSingleton<MessageSender>();
services.AddSingleton<ISignCommandsExecutor, SignCommandsExecutor>();

var serviceProvider = services.BuildServiceProvider();
var serverController = serviceProvider.GetService<IServerController>();
var clientSignCommandsExecutor = serviceProvider.GetService<ISignCommandsExecutor>();
MessageReceiver.Initialize(clientSignCommandsExecutor);

serverController.Start(7777, 10);
Console.ReadLine();
serverController.Stop();