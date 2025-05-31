using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.RequestMapping;

public class DefaultMessageHandlerMapping : IMessageHandlerMapping
{
    private static readonly ConcurrentDictionary<Type, Type> _handlers = new();
    public Type GetHadlerInvokerDescription(Type requestType)
    {
        return _handlers.GetOrAdd(requestType, static (type) =>
        {
            var resultType = type.GetInterface(typeof(IMessage<>).Name).GenericTypeArguments[0];
            var invokerType = typeof(IMessageHandlerInvoker<,>).MakeGenericType(type, resultType);
            return invokerType;
        });
    }
}
