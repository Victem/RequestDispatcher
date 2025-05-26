using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.DependencyInjection;

using RequestDispatcher.Core;
using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;
using RequestDispatcher.Core.Processing.Requests;
using RequestDispatcher.Core.Processing.Sreams;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Benchmark;

[MemoryDiagnoser(true)]
public class Benchmarks
{
    private IDispatcher _dispatcher;
    private Ping _ping = new Ping("Hello World");
    private FirstMessage _firstMessage = new FirstMessage();
    private StreamRequest _streamRequest = new StreamRequest();
    private int[] _numbers;
    [GlobalSetup]
    public async Task GlobalSetup()
    {
        var services = new ServiceCollection();
        services.AddDispatcher();
        services.AddScoped<IRequestHandler<Ping, Pong>, PingHandler>();
        services.AddScoped<IMessageHandler<FirstMessage, MessageHandled>, MessageHandler>();
        services.AddScoped<IStreamRequestHandler<StreamRequest, StreamResult>, StreamRequestHandler>();
        //services.AddSingleton<IRequestHanlderMapping, CustomHanlderMapping>();
        var provider = services.BuildServiceProvider();

        _dispatcher = provider.GetRequiredService<IDispatcher>();
        _numbers = [1,2,3,4,5,6,7,8,9,10];
    }

    [Benchmark]
    public async Task RequestHandling()
    {
        await _dispatcher.Send(_ping);
    }

    [Benchmark]
    public async Task EventHandling()
    {
        await _dispatcher.Publish(_firstMessage);
    }

    [Benchmark]
    public async Task StreamHandling()
    {
        await foreach (var item in _dispatcher.CreateStream(_streamRequest))
        { }
    }

    [Benchmark]
    public async Task StreamSimple()
    {
        await foreach (var item in ReadNumbers())
        { }
    }

    private async IAsyncEnumerable<int> ReadNumbers() 
    {
        foreach (var item in _numbers)
        {
            yield return item;
        }
    }
}
