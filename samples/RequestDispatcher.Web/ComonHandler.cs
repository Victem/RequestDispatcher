using RequestDispatcher.Core.Contracts;
using RequestDispatcher.Core.Processing.Requests;

namespace RequestDispatcher.Web;


public record struct TestRequest(string Message) : IRequest<TestResult>;
public record struct TestResult(int Number);

[RegisterScoped(Registration = RegistrationStrategy.SelfWithProxyFactory)]
[RegisterSingleton(Registration = RegistrationStrategy.SelfWithProxyFactory)]
public class ComonHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    public ValueTask<TResult> Handle(TRequest request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
