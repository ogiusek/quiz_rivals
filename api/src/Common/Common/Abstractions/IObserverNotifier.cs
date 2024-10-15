namespace Common.Abstractions;

public interface IObserverNotifier<T>
{
  Task Notify(T value);
}