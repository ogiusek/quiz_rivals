using Common.Types;

namespace Common.App.Abstractions;

public abstract class CustomCommandHandler<TCommand> : ICustomCommandHandler<TCommand>
  where TCommand : class, ICustomCommand
{
  bool ICustomCommandHandler<TCommand>.RunAfterExecute { get; set; } = false;

  public abstract Task<Res> Execute(TCommand command);
}