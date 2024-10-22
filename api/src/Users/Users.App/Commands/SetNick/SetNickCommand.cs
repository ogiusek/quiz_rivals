using Common.App.Abstractions;
using Common.Exceptions;
using Common.Extensions;
using Common.Types;
using Users.Core.Models.UserModel;

namespace Users.App.Commands.SetNick;

public record SetNickCommand(string Id, string Nick) : ICustomCommand
{
  public Res Validate()
  {
    Res res = Res.Success();
    res.Fail(UserNick.Validate(Nick));
    return res;
  }
}