namespace Common.Abstractions;

public interface IClock
{
  DateTime Now { get; }
}