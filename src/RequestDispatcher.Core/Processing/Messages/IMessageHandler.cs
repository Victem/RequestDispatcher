using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Messages;

public interface IMessageHandler<TEvent, TEmpty> where TEvent : IMessage<TEmpty>
{
    ValueTask<TEmpty> Handle(IMessage<TEmpty> message, CancellationToken cancellationToken = default);
}
