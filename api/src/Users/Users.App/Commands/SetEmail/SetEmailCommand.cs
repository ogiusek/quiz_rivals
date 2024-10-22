using Common.App.Abstractions;
using Common.Exceptions;
using Common.Extensions;
using Common.Types;
using Common.ValueObjects;
using Users.Core.Models.UserModel;

namespace Users.App.Commands.SetEmail;

public record SetEmailCommand(string Id, string Email) : ICustomCommand
{
  public Res Validate()
  {
    Res res = Res.Success();

    res.Fail(Common.ValueObjects.Email.Validate(Email));
    return res;
  }
}