using Common.Abstractions;

namespace Common.AbstractionsImplementations;

public class Clock : IClock
{
  public DateTime Now => DateTime.UtcNow;
}