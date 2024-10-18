using Common.Abstractions;
using Common.Extensions;
using Common.Types;

namespace Common.ValueObjects;

public class Id : IValueObject<string>
{
  public string Value { get; }

  public Id(string value)
  {
    Value = value;
    Validate(value).Throw();
  }

  public static Id New() => new Id(Guid.NewGuid().ToString());

  public static Res Validate(string value)
  {
    Res res = Res.Success();
    if (string.IsNullOrWhiteSpace(value))
    {
      res.Exceptions = res.Exceptions.Append(new ArgumentException("Id cannot be null or empty", nameof(value)));
    }
    return res;
  }
};