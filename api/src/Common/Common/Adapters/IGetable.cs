namespace Common.Adapters;

public interface IGetable<T>
{
  IQueryable<T> Get { get; }
}