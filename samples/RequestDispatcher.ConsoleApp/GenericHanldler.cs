using Microsoft.Extensions.DependencyInjection;

using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Requests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.ConsoleApp;

public class GenericHanldler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : class
{
    private readonly IServiceProvider _serviceProvider;

    public GenericHanldler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ValueTask<TResult> Handle(TRequest request, CancellationToken token = default)
    {
        var handler = _serviceProvider.GetRequiredKeyedService<IRequestHandler<TRequest,TResult>>(request.GetType().Name);
        var result = handler.Handle(request, token);
        //var result = handler.GetType().GetMethod("Handle").InitInvoke(handler, [request, token]);
        return (ValueTask<TResult>)result;
    }
}
