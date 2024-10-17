using Common.Types;

namespace Common.Abstractions;

public interface IObserverListener
{
  // summary:
  //   Subscribes to events.
  // returns:
  //   Res (fails if EventListener is already subscribed)
  public Res Subscribe(EventListener messageListener);

  // summary:
  //   Unsubscribes from events.
  // returns:
  //   Res (fails if EventListener is not subscribed)
  public Res Unsubscribe(Id messageListenerId);
}

public interface IObserverListener<T>
{
  // summary:
  //   Subscribes to events.
  // returns:
  //   Res (fails if EventListener is already subscribed)
  public Res Subscribe(EventListener<T> messageListener);

  // summary:
  //   Unsubscribes from events.
  // returns:
  //   Res (fails if EventListener is not subscribed)
  public Res Unsubscribe(Id messageListenerId);
}