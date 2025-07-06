using RequestDispatcher.Core.Contracts;

namespace RequestDispatcher.Web.Handlers;

public record RequestOne(string Text) : IRequest<RequestOneResult>;


