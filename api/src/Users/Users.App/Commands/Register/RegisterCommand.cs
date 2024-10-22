using Common.App.Abstractions;
using Common.Extensions;
using Common.Types;
using Users.Core.Models.UserModel;

namespace Users.App.Commands.Register;

public record RegisterCommand(
  string Nick,
  string Password
) : ICustomCommand
{
  public Res Validate()
  {
    Res res = Res.Success();
    res.Fail(UserNick.Validate(Nick));
    res.Fail(UserPassword.Validate(Password));
    return res;
  }
}