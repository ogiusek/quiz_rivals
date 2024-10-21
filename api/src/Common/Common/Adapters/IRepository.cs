namespace Common.Adapters;

public interface IRepository<T> :
  IGetable<T>,
  IAddable<T>,
  IUpdatable<T>,
  IRemovable<T>
{
  public static IEnumerable<Type> Interfaces => [typeof(IRepository<T>), typeof(IGetable<T>), typeof(IAddable<T>), typeof(IUpdatable<T>), typeof(IRemovable<T>)];
}