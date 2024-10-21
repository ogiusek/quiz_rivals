using Common.Abstractions;
using Common.Extensions;
using Common.Types;

namespace Users.Core.Models.UserModel;

public sealed class UserPassword : IValueObject<string>
{
  public string Value { get; }

  public UserPassword(string value)
  {
    Value = value;
  }

  public static Res Validate(string value)
  {
    Res res = Res.Success();

    if (value.Length < 8)
      res.Fail(new ArgumentException("Password must be at least 8 characters long"));

    return res;
  }
}