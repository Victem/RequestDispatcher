using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Behaviors;

public class MetricsBehavior<TRequest, TResult> : IRequestBasePipelineBehavior<TRequest, TResult> where TRequest : IRequestBase<TResult>
{
    public int Order { get; } = int.MaxValue;

    public ValueTask<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        return next();
    }
}
