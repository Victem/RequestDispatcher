using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Benchmark;

public record FirstMessage : IMessage<MessageHandled>;
public class MessageHandler : IMessageHandler<FirstMessage, MessageHandled>
{
    public ValueTask<MessageHandled> Handle(IMessage<MessageHandled> message, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(new MessageHandled());
    }
}
