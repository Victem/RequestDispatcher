using RequestDispatcher.Core.Contracts;

namespace RequestDispatcher.Core.Processing.Requests;

internal interface IInitRequestInvoke<TResult>
{
    ValueTask<TResult> InitInvoke(IRequest<TResult> request, CancellationToken cancellationToken = default);
}

