using Common.Types;

namespace Common.App.Interfaces;

public abstract class ValueObject<T>
{
  public T Value { get; private set; }

  protected ValueObject(T value)
  {
    Value = value;
  }


  // public static implicit operator ValueObject<T>(T value) => new ValueObject<T>(value);
  public static implicit operator T(ValueObject<T> vo) => vo.Value;

  public bool Equals(ValueObject<T> other) { return Value.Equals(other.Value); }

  public static bool operator ==(ValueObject<T> left, ValueObject<T> right) => left.Equals(right);
  public static bool operator !=(ValueObject<T> left, ValueObject<T> right) => !left.Equals(right);

  public override bool Equals(object obj)
    => base.Equals(obj);

  public override int GetHashCode()
    => base.GetHashCode();

  public override string ToString()
    => Value.ToString();

}