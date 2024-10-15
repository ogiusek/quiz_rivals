using Common.Types;

namespace Common.App.Abstractions;

public interface ICustomCommandHandler<TCommand>
  where TCommand : ICustomCommand
{
  Task<Res> HandleAsync(TCommand command);
  Task PostHandleAsync() => Task.CompletedTask;
}