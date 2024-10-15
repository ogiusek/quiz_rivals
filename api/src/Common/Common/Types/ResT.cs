namespace Common.Types;

public sealed class Res<T> : Res
{
  private T? _value;

  public T? Value => IsSuccess ? _value : default;

  public Res(T value) : base()
  {
    _value = value;
  }

  public Res(IEnumerable<Exception> exceptions) : base(exceptions)
  { }
}