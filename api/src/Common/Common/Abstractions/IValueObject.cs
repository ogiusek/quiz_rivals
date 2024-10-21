using Common.Types;

namespace Common.Abstractions;

public interface IValueObject<T>
{
  public T Value { get; }

  protected static T Format(T value) => value;
  public static abstract Res Validate(T value);
}
