using Common.Types;

namespace Common.App.Abstractions;

public interface ICustomQuery<TResponse>
  where TResponse : ICustomQueryResponse
{
  public Res Validate() => Res.Success();
}