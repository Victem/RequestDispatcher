using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Sreams;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Behaviors;

public class LoggingStreamBehavior<TRequest, TResult> : IStreamRequestPipelineBehavior<TRequest, TResult> where TRequest : IStreamRequest<TResult>
{
    public int Order { get; }

    public async IAsyncEnumerable<TResult> Handle(TRequest request, StreamHandlerDelegate<TResult> next, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        //Console.WriteLine("Before");
        await foreach (var item in next().WithCancellation(cancellationToken)) 
        {
            yield return item;
        }
    }
}
