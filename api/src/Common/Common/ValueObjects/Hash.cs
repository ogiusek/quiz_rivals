using Common.Abstractions;
using Common.Extensions;
using Common.Types;

namespace Common.ValueObjects;

public sealed class Hash : IValueObject<string>
{
  public string Value { get; }

  public Hash(string value)
  {
    Value = value;
    Validate(Value).Throw();
  }

  public static Res Validate(string value)
  {
    Res res = Res.Success();

    if (string.IsNullOrWhiteSpace(value))
    {
      res.Fail(new ArgumentException("Password hash cannot be null or empty"));
    }

    return res;
  }
}