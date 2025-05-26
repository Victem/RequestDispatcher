using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Sreams;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.RequestMapping;

public class DefaultStreamRequestHandlerMapping : IStreamRequestHanlderMapping
{
    private static readonly ConcurrentDictionary<Type, Type> _handlers = new();
    public Type GetHadlerInvokerDescription(Type requestType)
    {
        return _handlers.GetOrAdd(requestType, static (type) =>
        {
            var resultType = type.GetInterface(typeof(IStreamRequest<>).Name).GenericTypeArguments[0];
            var invokerType = typeof(IStreamRequestHandlerInvoker<,>).MakeGenericType(type, resultType);
            return invokerType;

        });
    }
}
