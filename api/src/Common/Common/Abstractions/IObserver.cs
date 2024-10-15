namespace Common.Abstractions;

public interface IObserver<T> : IObserverNotifier<T>, IObserverListener<T>
{ }