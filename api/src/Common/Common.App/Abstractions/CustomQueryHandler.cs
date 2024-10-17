using Common.Types;

namespace Common.App.Abstractions;

public abstract class CustomQueryHandler<TQuery, TResponse> : ICustomQueryHandler<TQuery, TResponse>
  where TResponse : ICustomQueryResponse
  where TQuery : ICustomQuery<TResponse>
{
  public abstract Task<Res<TResponse>> Execute(TQuery query);
}