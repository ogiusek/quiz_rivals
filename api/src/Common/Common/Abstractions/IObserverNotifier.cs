namespace Common.Abstractions;

public interface IObserverNotifier
{
  Task Notify();
}

public interface IObserverNotifier<T>
{
  Task Notify(T value);
}