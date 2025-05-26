using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Behaviors;

//public interface IRequestBasePipelineBehavior<TRequest, TEmprtyResult> where TRequest : IRequest<TEmprtyResult>
public class LoggingBehavior<TRequest, TResult> : IRequestBasePipelineBehavior<TRequest, TResult> where TRequest : IRequestBase<TResult>
{
    public int Order { get; }

    public ValueTask<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        return next();
    }
}
