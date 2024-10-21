using Common.Types;

namespace Common.Adapters;

public interface IAddable<T>
{
  Task<Res> Add(T model) => Add([model]);
  Task<Res> Add(IEnumerable<T> models);
}