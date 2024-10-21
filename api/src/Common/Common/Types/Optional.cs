namespace Common.Types;

public sealed class Optional<T>
{
  public bool HasValue { get; }
  public T? Value { get; }

  public Optional(T value)
  {
    HasValue = true;
    Value = value;
  }

  public Optional()
  {
    HasValue = false;
    Value = default;
  }
}