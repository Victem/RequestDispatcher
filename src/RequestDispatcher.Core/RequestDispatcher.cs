using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;
using RequestDispatcher.Core.Processing.Requests;
using RequestDispatcher.Core.Processing.Sreams;
using RequestDispatcher.Core.RequestMapping;

using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core;

public sealed class RequestDispatcher : IRequestDispatcher
{
    
    private readonly IServiceProvider _serviceProvider;
    
    private readonly IRequestHanlderMapping _requestHanlderMapping;
    private readonly IMessageHandlerMapping _messageHandlerMapping;    
    private readonly IStreamRequestHanlderMapping _streamRequestHandlerMapping;
    public RequestDispatcher(
        IServiceProvider serviceProvider,
        IRequestHanlderMapping hanlderMapping,
        IMessageHandlerMapping messageHandlerMapping,
        IStreamRequestHanlderMapping streamRequestHandlerMapping
        )
    {
        _serviceProvider = serviceProvider;
        _requestHanlderMapping = hanlderMapping;
        _messageHandlerMapping = messageHandlerMapping;
        _streamRequestHandlerMapping = streamRequestHandlerMapping;
    }


    public ValueTask<TEmptyResult> Publish<TEmptyResult>(IMessage<TEmptyResult> message, CancellationToken cancellationToken = default)
    {
        var descriptor = _messageHandlerMapping.GetHadlerInvokerDescription(message.GetType());
        var invoker = _serviceProvider.GetRequiredService(descriptor);
        return ((IInitMessageInvoke<TEmptyResult>)invoker).InitInvoke(message, cancellationToken);
    }

    public ValueTask<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();        
        var descriptor = _requestHanlderMapping.GetHadlerInvokerDescription(requestType);        
        var invoker = _serviceProvider.GetRequiredService(descriptor);
        return ((IInitRequestInvoke<TResult>)invoker).InitInvoke(request, cancellationToken);
    }

    public IAsyncEnumerable<TResult> CreateStream<TResult>(IStreamRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var descriptor = _streamRequestHandlerMapping.GetHadlerInvokerDescription(requestType);
        var invoker = _serviceProvider.GetRequiredService(descriptor);
        return ((IInitSreamRequestInvoke<TResult>)invoker).InitInvoke(request, cancellationToken);
    }
}
