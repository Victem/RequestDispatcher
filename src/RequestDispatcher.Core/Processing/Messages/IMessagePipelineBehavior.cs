using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Messages;

public interface IMessagePipelineBehavior<TRequest, TEmptyResult> : IRequestBasePipelineBehavior<TRequest, TEmptyResult> where TRequest : IMessage<TEmptyResult>
{
}
