namespace Domain.Core.Commands;

public class CommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
}
