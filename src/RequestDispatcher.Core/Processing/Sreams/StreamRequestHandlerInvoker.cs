using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Requests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Sreams;

internal class StreamRequestHandlerInvoker<TRequest, TResult> :
    IStreamRequestHandlerInvoker<TRequest, TResult>,
    IInitSreamRequestInvoke<TResult> where TRequest : IStreamRequest<TResult>
{
    private readonly IStreamRequestHandler<TRequest, TResult> _handler;
    private readonly IStreamRequestPipelineBehavior<TRequest, TResult>[] _behaviors;

    public StreamRequestHandlerInvoker(
        IStreamRequestHandler<TRequest, TResult> handler,
        IEnumerable<IStreamRequestPipelineBehavior<TRequest, TResult>> behaviors)

    {
        _handler = handler;
        _behaviors = behaviors.OrderBy(b => b.Order).ToArray();
    }


    public IAsyncEnumerable<TResult> Invoke(TRequest request, CancellationToken token = default)
    {
        StreamHandlerDelegate<TResult> handler = () => _handler.Handle(request, token);

        for (int i = 0; i < _behaviors.Length; i++)
        {
            var handlerCopy = handler;
            var pipelineCopy = _behaviors[i];
            handler = () => pipelineCopy.Handle(request, handlerCopy, token);
        }

        return handler.Invoke();
    }
    public IAsyncEnumerable<TResult> InitInvoke(IStreamRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        return Invoke((TRequest) request, cancellationToken);
    }
}