using Common.Types;

namespace Common.App.Abstractions;

public interface ICustomQueryHandler<TQuery, TResponse>
  where TResponse : ICustomQueryResponse
  where TQuery : ICustomQuery<TResponse>
{
  sealed Task<Res<TResponse>> Handle(TQuery query)
  {
    Res res = query.Validate();
    return res.IsSuccess ?
      Execute(query) :
      Task.FromResult(new Res<TResponse>(res.Exceptions));
  }

  protected Task<Res<TResponse>> Execute(TQuery query);
}