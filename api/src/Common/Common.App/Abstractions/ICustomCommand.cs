using Common.Types;

namespace Common.App.Abstractions;

public interface ICustomCommand
{
  public Res Validate() => Res.Success();
}