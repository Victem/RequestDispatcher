using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Processing.Requests;

namespace RequestDispatcher.Web.Handlers;

[RegisterScoped(Registration = RegistrationStrategy.SelfWithProxyFactory)]
public class RequestOneHandler : IRequestHandler<RequestOne, RequestOneResult>
{
    private readonly IRequestDispatcher _dispatcher;

    public RequestOneHandler(IRequestDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async ValueTask<RequestOneResult> Handle(RequestOne request, CancellationToken token = default)
    {
        await _dispatcher.Publish(new MessageOne(request.Text));
        return new RequestOneResult(request.Text.Length);
    }
}
