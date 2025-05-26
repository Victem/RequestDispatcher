using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Abstractions;

public delegate ValueTask<TResult> RequestHandlerDelegate<TResult>(CancellationToken t = default);

public interface IRequestBasePipelineBehavior<TRequest, TResult> where TRequest : IRequestBase<TResult>
{
     int Order { get; }

     ValueTask<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken);
}