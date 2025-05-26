using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;
using RequestDispatcher.Core.Processing.Requests;
using RequestDispatcher.Core.Processing.Sreams;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RequestDispatcher.ConsoleApp;

public record FirstRequest() : IRequest<FirstResponse>;
public record FirstResponse();

public record SecondRequest() : IRequest<SecondResponse>;
public record SecondResponse();

public record FirstMessage() : IMessage<MessageHandled>;
public record StreamRequest() : IStreamRequest<string>;

//[RegisterScoped(ImplementationType = typeof(FirstHandler))]
//[RegisterScoped(Registration = RegistrationStrategy.ImplementedInterfaces, ImplementationType = typeof(FirstHandler), Duplicate = DuplicateStrategy.Skip)]
[RegisterScoped(Registration = RegistrationStrategy.SelfWithProxyFactory)]
public class FirstHandler :
    IRequestHandler<FirstRequest, FirstResponse>,
    IRequestHandler<SecondRequest, SecondResponse>,
    IMessageHandler<FirstMessage, MessageHandled>
    
{

    public FirstHandler()
    {
    }
    public ValueTask<FirstResponse> Handle(FirstRequest request, CancellationToken token = default)
    {
        return ValueTask.FromResult(new FirstResponse());
    }

    public ValueTask<SecondResponse> Handle(SecondRequest request, CancellationToken token = default)
    {
        return ValueTask.FromResult(new SecondResponse());
    }

    public async ValueTask<MessageHandled> Handle(IMessage<MessageHandled> message, CancellationToken cancellationToken = default)
    {
        await Task.Delay(3000);
        return new MessageHandled();
    }
}

[RegisterScoped(Registration = RegistrationStrategy.SelfWithProxyFactory, Duplicate = DuplicateStrategy.Append)]
public class SecondHandler :
    IMessageHandler<FirstMessage, MessageHandled>

{
    public ValueTask<MessageHandled> Handle(IMessage<MessageHandled> message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}


public class StreamHandler : IStreamRequestHandler<StreamRequest, string>
{
    public async IAsyncEnumerable<string> Handle(StreamRequest request, [EnumeratorCancellation]CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            yield return $"{nameof(StreamHandler)}: {DateTime.Now}";
            await Task.Delay(1000);
        }
    }
}
