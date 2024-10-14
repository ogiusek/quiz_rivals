namespace Common.App.Abstractions;

public interface ICustomQueryHandler<TQuery, TResponse>
  where TResponse : ICustomQueryResponse
  where TQuery : ICustomQuery<TResponse>
{
  Task<TResponse> HandleAsync(TQuery query);
}