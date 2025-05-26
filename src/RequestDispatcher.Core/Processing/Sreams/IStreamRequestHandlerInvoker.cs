using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Sreams;

internal interface IStreamRequestHandlerInvoker<TRequest, TResult>
{
    IAsyncEnumerable<TResult> Invoke(TRequest request, CancellationToken cancellationToken = default);
}
