using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Sreams;

public delegate IAsyncEnumerable<TResponse> StreamHandlerDelegate<out TResponse>();


public interface IStreamRequestPipelineBehavior<TRequest, TResult> where TRequest : IStreamRequest<TResult>
{
    int Order { get; }
    IAsyncEnumerable<TResult> Handle(TRequest request, StreamHandlerDelegate<TResult> next, CancellationToken cancellationToken = default);
}
