using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Requests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Messages;

public class MessageHandlerInvoker<TRequest, TEmptyResult> :
    IMessageHandlerInvoker<TRequest, TEmptyResult>,
    IInitMessageInvoke<TEmptyResult> where TRequest : IMessage<TEmptyResult>

{
    
    private readonly IRequestBasePipelineBehavior<TRequest, TEmptyResult>[] _behaviors;
    private readonly IEnumerable<IMessageHandler<TRequest, TEmptyResult>> _messageHandlers;

    public MessageHandlerInvoker(        
        IEnumerable<IRequestBasePipelineBehavior<TRequest, TEmptyResult>> baseBehaviors,
        IEnumerable<IMessagePipelineBehavior<TRequest, TEmptyResult>> messageBehaviors,
        IEnumerable<IMessageHandler<TRequest, TEmptyResult>> messagehandlers
        )
    {
        
        _behaviors = messageBehaviors.Select(mb => (IRequestBasePipelineBehavior<TRequest, TEmptyResult>)mb)
            .Concat(baseBehaviors)
            .OrderBy(b => b.Order)
            .ToArray();

        _messageHandlers = messagehandlers;
    }




    public async ValueTask<TEmptyResult> Invoke(TRequest request, CancellationToken cancellationToken = default)
    {

        //await Parallel.ForEachAsync(_messageHandlers, cancellationToken,async (handler, token)=>
        //{
        //    await InvokeHandler(request, handler, token);
        //    //return ValueTask.CompletedTask;
        //});

        //var tasks =_messageHandlers.Select(mh => mh.Handle(request, cancellationToken).AsTask()).ToArray();
        //await Task.WhenAll(tasks);
        foreach (var item in _messageHandlers)
        {
            await item.Handle(request, cancellationToken);
        }
        return default;
    }

    private ValueTask<TEmptyResult> InvokeHandler(TRequest request, IMessageHandler<TRequest, TEmptyResult> handler,  CancellationToken cancellationToken)
    {
        RequestHandlerDelegate<TEmptyResult> handlerReference = (t) => handler.Handle(request, cancellationToken);

        for (int i = 0; i < _behaviors.Length; i++)
        {
            var handlerCopy = handlerReference;
            var pipelineCopy = _behaviors[i];
            handlerReference = (t) => pipelineCopy.Handle(request, handlerCopy, cancellationToken);
        }

        return handlerReference.Invoke(cancellationToken);
    }

    public ValueTask<TEmptyResult> InitInvoke(IMessage<TEmptyResult> request, CancellationToken cancellationToken = default)
    {
        return Invoke((TRequest)request, cancellationToken);
    }
}