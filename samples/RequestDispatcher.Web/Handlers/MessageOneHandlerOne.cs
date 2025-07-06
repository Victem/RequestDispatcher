using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;

namespace RequestDispatcher.Web.Handlers;

[RegisterScoped(Duplicate = DuplicateStrategy.Append, Registration = RegistrationStrategy.SelfWithProxyFactory)]
public class MessageOneHandlerOne : IMessageHandler<MessageOne, MessageHandled>
{
    public async ValueTask<MessageHandled> Handle(MessageOne message, CancellationToken cancellationToken = default)
    {
        return default;
    }
}
