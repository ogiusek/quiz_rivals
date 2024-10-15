using Common.Types;

namespace Common.App.Abstractions;

public interface IValueObject<T>
{
  public T Value { get; }

  public static abstract Res Validate(T value);
}
