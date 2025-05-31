using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;
using RequestDispatcher.Core.Processing.Requests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.ConsoleApp;

public interface IAppRequest<TResult> : IRequest<TResult>
{

}

public record struct Ok { };

public interface IAppMessage : IMessage<Ok>
{

}


public record FirstAppRequest() : IAppRequest<FirstAppResult>;
public record FirstAppResult();

public record FirstAppMessage() : IAppMessage;

[RegisterSingleton(Registration = RegistrationStrategy.SelfWithProxyFactory)]
public class NestedHandler :
    IRequestHandler<FirstAppRequest, FirstAppResult>,
    IMessageHandler<FirstAppMessage, Ok>
    
{
    public ValueTask<FirstAppResult> Handle(FirstAppRequest request, CancellationToken token = default)
    {
        return new ValueTask<FirstAppResult>();
    }

    public ValueTask<Ok> Handle(IMessage<Ok> message, CancellationToken cancellationToken = default)
    {
        return new ValueTask<Ok>();
    }
}
