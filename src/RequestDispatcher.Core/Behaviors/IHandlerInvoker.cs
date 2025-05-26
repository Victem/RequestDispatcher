using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Behaviors;

public interface IHandlerInvoker<TRequest, TResult>
{
    ValueTask<TResult> Invoke(TRequest request, CancellationToken cancellationToken = default);
}
