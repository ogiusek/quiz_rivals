namespace Common.Abstractions;

public interface IObserver : IObserverNotifier, IObserverListener
{ }

public interface IObserver<T> : IObserverNotifier<T>, IObserverListener<T>
{ }