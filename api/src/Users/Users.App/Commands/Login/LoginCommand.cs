using Common.App.Abstractions;
using Common.Extensions;
using Common.Types;
using Users.Core.Models.UserModel;

namespace Users.App.Commands.Login;

#nullable enable
public record LoginCommand(
  string Nick,
  string? Password
) : ICustomCommand
{
  public Res Validate()
  {
    Res res = Res.Success();
    res.Fail(UserNick.Validate(Nick));
    if (Password is not null)
      res.Fail(UserPassword.Validate(Password));
    return res;
  }
}

#nullable restore