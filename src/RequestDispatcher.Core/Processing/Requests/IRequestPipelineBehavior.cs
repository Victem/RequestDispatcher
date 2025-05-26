using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Processing.Requests;

public interface IRequestPipelineBehavior<TRequest, TResult> : IRequestBasePipelineBehavior<TRequest, TResult> where TRequest : IRequest<TResult>
{
    
}
