// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using RequestDispatcher.ConsoleApp;
using RequestDispatcher.Core;
using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;
using RequestDispatcher.Core.Processing.Requests;
using RequestDispatcher.Core.Processing.Sreams;

var services = new ServiceCollection();

services.AddDispatcher();
services.AddTransient(sp => new DataProvider());
services.AddKeyedScoped<IRequestHandler<FirstRequest, FirstResponse>, FirstHandler>(nameof(FirstRequest));
services.AddRequestDispatcherConsoleApp();
services.AddScoped<IStreamRequestHandler<StreamRequest, string>, StreamHandler>();
services.AddKeyedSingleton<IMessageHandler<FirstMessage, MessageHandled>,FirstHandler>("one");
services.AddKeyedSingleton<IMessageHandler<FirstMessage, MessageHandled>,SecondHandler>("one");

var serviceProvider = services.BuildServiceProvider();

var dispatcher = serviceProvider.GetRequiredService<IRequestDispatcher>();

var handlers = serviceProvider.GetKeyedServices<IMessageHandler<FirstMessage, MessageHandled>>("one").ToArray();


//var firstResult = await dispatcher.Send(new FirstRequest());
//var secondResult = await dispatcher.Send(new SecondRequest());
await dispatcher.Send(new FirstAppRequest());
//await dispatcher.Publish(new FirstAppMessage());

var dataProviderOne = serviceProvider.GetRequiredService<DataProvider>();
var dataProviderTwo = serviceProvider.GetRequiredService<DataProvider>();
dataProviderOne.Name = $"{nameof(dataProviderOne)}";
dataProviderTwo.Name = $"{nameof(dataProviderTwo)}";
CancellationTokenSource cts = new CancellationTokenSource();

var dataProviders = new List<DataProvider>
{ 
    dataProviderOne,
    dataProviderTwo
};


/*await Parallel.ForEachAsync(dataProviders, cts.Token, async (ds, t) =>
{
    await foreach (var item in ds.ReadData(t))
    {
        WriteData(item);
    }
});*/

//await foreach(var item in dispatcher.CreateStream(new StreamRequest(), cts.Token).ConfigureAwait(false))
//{ 
//    WriteData(item);
//}


//var task1 = Task.Run(async() => 
//{
//    await foreach (var item in dataProviderOne.ReadData(cts.Token)) 
//    {
//        WriteData(item);
//    }
//});


//var task2 = Task.Run(async () => 
//{
//    await foreach (var item in dataProviderTwo.ReadData(cts.Token))
//    {
//        WriteData(item);
//    }
//});

//Task.WaitAll(task1, task2);


Task WriteData(string data) 
{
    Console.WriteLine(data);
    return Task.CompletedTask;
}











