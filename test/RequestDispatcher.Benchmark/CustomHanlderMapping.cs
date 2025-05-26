using RequestDispatcher.Benchmark;
using RequestDispatcher.Core.Processing.Requests;
using RequestDispatcher.Core.RequestMapping;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomHanlderMapping : IRequestHanlderMapping
{
    private static Dictionary<Type, Type> _requestMap = new Dictionary<Type, Type>
    {
        [typeof(Ping)] = typeof(RequestHandlerInvoker<Ping, Pong>),
        //[typeof(SecondRequest)] = typeof(HandlerInvoker<SecondRequest, SecondResponse>),
    }
    ;
    public Type GetHadlerInvokerDescription(Type requestType)
    {
        return _requestMap[requestType];
    }
}
