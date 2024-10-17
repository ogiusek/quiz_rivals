using System.Collections.Concurrent;
using Common.Types;

namespace Common.AbstractionsImplementations;

public sealed class Observer : Abstractions.IObserver
{
  ConcurrentDictionary<Id, EventListener> _eventListeners = new();

  public async Task Notify()
  {
    await Task.WhenAll(_eventListeners.Values
      .ToList()
      .Select(listener => listener.Action()));
  }

  public Res Subscribe(EventListener messageListener)
  {
    bool added = _eventListeners.TryAdd(messageListener.Id, messageListener);
    return added ?
      Res.Success() :
      Res.Fail(new Exception("Already subscribed"));
  }

  public Res Unsubscribe(Id messageListenerId)
  {
    EventListener? eventListener;
    _eventListeners.TryRemove(messageListenerId, out eventListener);
    return eventListener is null ?
      Res.Fail(new Exception("Cannot unsubscribe if not subscribed")) :
      Res.Success();
  }
}

public sealed class Observer<T> : Abstractions.IObserver<T>
{
  ConcurrentDictionary<Id, EventListener<T>> _eventListeners = new();

  public async Task Notify(T value)
  {
    await Task.WhenAll(_eventListeners.Values
      .ToList()
      .Select(listener => listener.Action(value)));
  }

  public Res Subscribe(EventListener<T> messageListener)
  {
    bool added = _eventListeners.TryAdd(messageListener.Id, messageListener);
    return added ?
      Res.Success() :
      Res.Fail(new Exception("Already subscribed"));
  }

  public Res Unsubscribe(Id messageListenerId)
  {
    EventListener<T>? eventListener;
    _eventListeners.TryRemove(messageListenerId, out eventListener);
    if (eventListener is null)
    {
      return Res.Fail(new Exception("Cannot unsubscribe if not subscribed"));
    }
    return Res.Success();
  }
}