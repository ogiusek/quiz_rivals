using System.Text.RegularExpressions;
using Common.Abstractions;
using Common.Extensions;
using Common.Types;

namespace Common.ValueObjects;

public sealed class Email : IValueObject<string>
{
  public string Value { get; }

  public Email(string value)
  {
    Value = Format(value);
    Validate(Value).Throw();
  }

  public static string Format(string value) => value;
  public static Res Validate(string value)
  {
    Res res = Res.Success();
    if (string.IsNullOrWhiteSpace(value))
      res.Fail(new ArgumentException("Email cannot be null or empty"));

    if (value.Length < 3)
      res.Fail(new ArgumentException("Email must be at least 3 characters long"));

    if (value.Length > 64)
      res.Fail(new ArgumentException("Email must be less than 64 characters long"));

    if (!Regex.IsMatch(value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
      res.Fail(new ArgumentException("Email must be a valid email address"));

    return res;
  }
}