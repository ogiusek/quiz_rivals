namespace Common.Types;

public record Id(string Value)
{
  public static Id New() => new Id(Guid.NewGuid().ToString());
};