using BenchmarkDotNet.Attributes;

using MediatR;

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
    private IRequestDispatcher _dispatcher;
    private IMediator _mediator;
    private Ping _ping = new Ping("Hello World");
    private Ping2 _ping2 = new Ping2("Hello World");
    private FirstMessage _firstMessage = new FirstMessage();
    private StreamRequest _streamRequest = new StreamRequest();
    private int[] _numbers;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        var services = new ServiceCollection();
        services.AddDispatcher();
        services.AddScoped<Core.Processing.Requests.IRequestHandler<Ping, Pong>, PingHandler>();
        services.AddScoped<IMessageHandler<FirstMessage, MessageHandled>, MessageHandler>();
        services.AddScoped<Core.Processing.Sreams.IStreamRequestHandler<StreamRequest, StreamResult>, StreamRequestHandler>();


        services.AddMediatR(options =>
        {
            options.AutoRegisterRequestProcessors = true;
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssembly(typeof(Benchmarks).Assembly);
        });
        //services.AddSingleton<IRequestHanlderMapping, CustomHanlderMapping>();


        var provider = services.BuildServiceProvider();

        _dispatcher = provider.GetRequiredService<IRequestDispatcher>();
        _numbers = [1,2,3,4,5,6,7,8,9,10];

        _mediator = provider.GetRequiredService<IMediator>();
    }

    [Benchmark]
    public async Task RequestHandling()
    {
        await _dispatcher.Send(_ping);
    }

    [Benchmark]
    public async Task MediatrRequestHandling()
    {
        await _mediator.Send(_ping2);
    }

    //[Benchmark]
    //public async Task EventHandling()
    //{
    //    await _dispatcher.Publish(_firstMessage);
    //}

    //[Benchmark]
    //public async Task StreamHandling()
    //{
    //    await foreach (var item in _dispatcher.CreateStream(_streamRequest))
    //    { }
    //}

    //[Benchmark]
    //public async Task StreamSimple()
    //{
    //    await foreach (var item in ReadNumbers())
    //    { }
    //}

    private async IAsyncEnumerable<int> ReadNumbers() 
    {
        foreach (var item in _numbers)
        {
            yield return item;
        }
    }
}
