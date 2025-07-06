using RequestDispatcher.Core.Contracts;

namespace RequestDispatcher.Web.Handlers;

public record class MessageOne(string Text) : IMessage<MessageHandled>;

