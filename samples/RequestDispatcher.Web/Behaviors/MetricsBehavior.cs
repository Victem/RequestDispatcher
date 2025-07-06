using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Contracts;

namespace RequestDispatcher.Web.Behaviors;

//<TRequest, TResult> where TRequest : IRequestBase<TResult>
[RegisterScoped(Registration = RegistrationStrategy.SelfWithProxyFactory)]
[RegisterSingleton(Registration = RegistrationStrategy.SelfWithProxyFactory)]
public class MetricsBehavior<TRequest, TResult> : IRequestBasePipelineBehavior<TRequest, TResult> where TRequest : IRequestBase<TResult>
{
    public int Order { get; }

    public async ValueTask<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var result = await next();
        return result;
    }
}
