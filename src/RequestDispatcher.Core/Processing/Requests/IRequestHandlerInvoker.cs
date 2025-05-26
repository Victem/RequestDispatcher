using RequestDispatcher.Core.Behaviors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Requests;

internal interface IRequestHandlerInvoker<TRequest, TResult> : IHandlerInvoker<TRequest,TResult>
{
}
