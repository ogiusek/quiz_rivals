using Common.Abstractions;

namespace Common.Tests.AbstractionsMocks;

public sealed class ClockMock : IClock
{
  private DateTime _now;
  public DateTime Now => _now;

  public ClockMock()
  {
    _now = new DateTime(2024, 10, 10);
  }

  public ClockMock(DateTime now)
  {
    _now = now;
  }
}