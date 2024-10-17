using Common.Types;

namespace Common.App.Abstractions;

public interface ICustomCommandHandler<TCommand>
  where TCommand : ICustomCommand
{
  protected bool RunAfterExecute { get; set; }

  public sealed Task<Res> Handle(TCommand command)
  {
    Res res = command.Validate();
    if (!res.IsSuccess)
    {
      RunAfterExecute = false;
      return Task.FromResult(res);
    }
    return Execute(command);
  }

  protected Task<Res> Execute(TCommand command);

  public sealed Task AfterHandle() => RunAfterExecute ?
    AfterExecute() :
    Task.CompletedTask;

  protected Task AfterExecute() => Task.CompletedTask;
}