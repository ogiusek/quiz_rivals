using Common.Types;

namespace Common.Adapters;

public interface IRemovable<T>
{
  Task<Res> Remove(T model) => Remove([model]);
  Task<Res> Remove(IEnumerable<T> models);
}