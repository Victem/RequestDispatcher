using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Sreams;

internal interface IInitSreamRequestInvoke<TResult> 
{
    IAsyncEnumerable<TResult> InitInvoke(IStreamRequest<TResult> request, CancellationToken cancellationToken = default);
}
