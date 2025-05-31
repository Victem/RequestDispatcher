using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Requests;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.RequestMapping;

internal class DefaultRequestHandlerMapping : IRequestHanlderMapping
{
    private static readonly ConcurrentDictionary<Type, Type> _handlers = new();
    public Type GetHadlerInvokerDescription(Type requestType)
    {
        return _handlers.GetOrAdd(requestType, static (type) =>
        {
            var resultType = type.GetInterface(typeof(IRequest<>).Name).GenericTypeArguments[0];
            var invokerType = typeof(IRequestHandlerInvoker<,>).MakeGenericType(type, resultType);
            return invokerType;
        });
    }
}
