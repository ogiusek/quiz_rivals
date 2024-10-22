using Common.Abstractions;
using Common.Extensions;
using Common.Types;

namespace Users.Core.Models.UserModel;

public sealed class UserNick : IValueObject<string>
{
  public string Value { get; }

  public UserNick(string value)
  {
    Value = Format(value);
    Validate(Value).Throw();
  }

  public static string Format(string value) => value.ToLower();
  public static Res Validate(string value)
  {
    Res res = Res.Success();

    if (string.IsNullOrWhiteSpace(value))
      res.Fail(new ArgumentException("Name cannot be null or empty"));

    if (value is null)
      return res;

    if (value.Length < 3)
      res.Fail(new ArgumentException("Name must be at least 3 characters long"));

    if (value.Length > 64)
      res.Fail(new ArgumentException("Name must be less than 64 characters long"));

    if (!value.All(c => char.IsLetter(c) || char.IsNumber(c) || c == '_'))
      res.Fail(new ArgumentException("Name must contain only letters"));

    return res;
  }
}