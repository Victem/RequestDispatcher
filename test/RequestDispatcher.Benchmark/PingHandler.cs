using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Requests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Benchmark;

public record Ping(string Message) : IRequest<Pong>;
public record Pong();

public class PingHandler : IRequestHandler<Ping, Pong>
{
    public ValueTask<Pong> Handle(Ping request, CancellationToken token = default)
    {
        return ValueTask.FromResult(new Pong());
    }
}

public record Ping2(string Message) : MediatR.IRequest<Pong2>;
public record Pong2();
public class PingHandler2 : MediatR.IRequestHandler<Ping2, Pong2>
{
    public Task<Pong2> Handle(Ping2 request, CancellationToken token = default)
    {
        return Task.FromResult(new Pong2());
    }
}