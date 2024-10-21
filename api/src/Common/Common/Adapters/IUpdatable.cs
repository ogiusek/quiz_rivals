using Common.Types;

namespace Common.Adapters;

public interface IUpdatable<T>
{
  Task<Res> Update(T model) => Update([model]);
  Task<Res> Update(IEnumerable<T> models);
}