using System.Net.WebSockets;
using Common.Abstractions;
using Common.Extensions;

namespace Common.Types;

public sealed class AppWebSocket : IAppWebSocket
{
  private Id _id;
  public Id Id => _id;
  public bool IsOpen => _webSocket.State == WebSocketState.Open;

  private readonly WebSocket _webSocket;
  private Dictionary<Id, EventListener<WebSocketMessage>> _eventListeners = new();

  public AppWebSocket(Id id, WebSocket webSocket)
  {
    _id = id;
    _webSocket = webSocket;

    if (webSocket.State != WebSocketState.Open)
    {
      throw new Exception("WebSocket is not open");
    }
  }

  public Task RunAsync() =>
    _webSocket.OnMessage((message) =>
      Task.WhenAll(_eventListeners.Values
        .ToList()
        .Select(listener => listener.Action(message)))
    , CancellationToken.None);

  public async Task<Res> Close()
  {
    if (!IsOpen)
    {
      return Res.Fail(new Exception("Socket already closed"));
    }

    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);

    return Res.Success();
  }

  public async Task<Res> Send(WebSocketMessage message)
  {
    if (!IsOpen)
    {
      return Res.Fail(new Exception("Socket already closed"));
    }

    string text = message.Message;

    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);

    await _webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

    return Res.Success();
  }

  public Res Subscribe(EventListener<WebSocketMessage> messageListener)
  {
    if (_eventListeners.ContainsKey(messageListener.Id))
    {
      return Res.Fail(new Exception("Already subscribed"));
    }

    _eventListeners.Add(messageListener.Id, messageListener);
    return Res.Success();
  }

  public Res Unsubscribe(Id messageListenerId)
  {
    if (!_eventListeners.ContainsKey(messageListenerId))
    {
      return Res.Fail(new Exception("Cannot unsubscribe if not subscribed"));
    }

    _eventListeners.Remove(messageListenerId);
    return Res.Success();
  }
}