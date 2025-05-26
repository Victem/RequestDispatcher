using RequestDispatcher.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core.Abstractions;

public interface IDispatcher
{
    public ValueTask<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default);    

    public ValueTask<TEmptyResult> Publish<TEmptyResult>(IMessage<TEmptyResult> message, CancellationToken cancellationToken = default);

    public IAsyncEnumerable<TResult> CreateStream<TResult>(IStreamRequest<TResult> request, CancellationToken cancellationToken = default);
}
