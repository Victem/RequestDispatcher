using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Requests;

public class RequestHandlerInvoker<TRequest, TResult> :
    IRequestHandlerInvoker<TRequest, TResult>,
    IInitRequestInvoke<TResult> where TRequest : IRequest<TResult>
{
    private readonly IRequestHandler<TRequest, TResult> _handler;
    private readonly IRequestBasePipelineBehavior<TRequest, TResult>[] _behaviors;
    //private int _behaviorsCount = 0;

    public RequestHandlerInvoker(
        IRequestHandler<TRequest, TResult> handler,
        IEnumerable<IRequestBasePipelineBehavior<TRequest, TResult>> baseBehaviors,
        IEnumerable<IRequestPipelineBehavior<TRequest, TResult>> requestBehaviors
        )
    {
        _handler = handler;
        _behaviors = requestBehaviors.Select(b => (IRequestBasePipelineBehavior<TRequest, TResult>)b)
            .Concat(baseBehaviors)
            .OrderBy(b => b.Order)
            .ToArray();

        //_behaviorsCount = ;
    }

    public ValueTask<TResult> Invoke(TRequest request, CancellationToken token = default)
    {
        //ValueTask<TEmprtyResult> Handler(CancellationToken t) => _handler.Handle(request, t);

        /*
         var handler = (MessageHandlerDelegate<TRequest, TResponse>)concreteHandler.Handle;

            foreach (var pipeline in pipelineBehaviours.Reverse())
            {
                var handlerCopy = handler;
                var pipelineCopy = pipeline;
                handler = (TRequest message, CancellationToken cancellationToken) => pipelineCopy.Handle(message, cancellationToken, handlerCopy);
            }

            _rootHandler = handler;
         */


        RequestHandlerDelegate<TResult> handler = (t) => _handler.Handle(request, token);

        //foreach (var behavior in _behaviors)
        //{
        //    var handlerCopy = handler;
        //    var pipelineCopy = behavior;
        //    handler = (t) => pipelineCopy.Handle(request, handlerCopy, token);
        //}

        for (int i = 0; i < _behaviors.Length; i++)
        {
            var handlerCopy = handler;
            var pipelineCopy = _behaviors[i];
            handler = (t) => pipelineCopy.Handle(request, handlerCopy, token);
        }

        return handler.Invoke(token);

        //var actionsChain = _behaviors.Aggregate(
        //    handler,
        //    (next, pipeline) => (t) => pipeline.Handle(request, next, token));

        //return actionsChain.Invoke(token);
    }

    public ValueTask<TResult> InitInvoke(IRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        return Invoke((TRequest)request, cancellationToken);
    }
}
