using RequestDispatcher.Core.Processing.Requests;
using RequestDispatcher.Core.RequestMapping;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.ConsoleApp;

public class CustomHanlderMapping : IRequestHanlderMapping
{
    private static Dictionary<Type, Type> _requestMap = new Dictionary<Type, Type>
    {
        [typeof(FirstRequest)] = typeof(RequestHandlerInvoker<FirstRequest, FirstResponse>),
        [typeof(SecondRequest)] = typeof(RequestHandlerInvoker<SecondRequest, SecondResponse>),
    }
    ;
    public Type GetHadlerInvokerDescription(Type requestType)
    {
        return _requestMap[requestType];
    }
}
