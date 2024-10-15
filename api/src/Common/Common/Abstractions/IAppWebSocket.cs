using Common.Types;

namespace Common.Abstractions;

public interface IAppWebSocket
{
  public Id Id { get; }
  public bool IsOpen { get; }
  public bool IsClosed => !IsOpen;

  public IObserverListener<WebSocketMessage> MessageListener { get; }

  // summary: 
  //   Sends a message.
  // returns:
  //   Res (fails if socket is closed)
  public Task<Res> Send(WebSocketMessage message);

  // summary:
  //   Closes the socket.
  // returns:
  //   Res (fails if socket is already closed)
  public Task<Res> Close();
}