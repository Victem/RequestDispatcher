using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Requests;

public interface IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    public ValueTask<TResult> Handle(TRequest request, CancellationToken token = default);
}
