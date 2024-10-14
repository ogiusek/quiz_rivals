using Common.Types;

namespace Common.Abstractions;

public interface IAppWebSocket
{
  public Id Id { get; }
  public bool IsOpen { get; }
  public bool IsClosed => !IsOpen;

  // summary: 
  //   Sends a message.
  // returns:
  //   Res (fails if socket is closed)
  public Task<Res> Send(WebSocketMessage message);

  // summary:
  //   Subscribes to events.
  // returns:
  //   Res (fails if EventListener is already subscribed)
  public Res Subscribe(EventListener<WebSocketMessage> messageListener);

  // summary:
  //   Unsubscribes from events.
  // returns:
  //   Res (fails if EventListener is not subscribed)
  public Res Unsubscribe(Id messageListenerId);

  // summary:
  //   Closes the socket.
  // returns:
  //   Res (fails if socket is already closed)
  public Task<Res> Close();
}