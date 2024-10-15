using System.Net.WebSockets;
using Common.Abstractions;
using Common.AbstractionsImplementations;
using Common.Extensions;

namespace Common.Types;

public sealed class AppWebSocket : IAppWebSocket
{
  private Id _id;
  public Id Id => _id;
  public bool IsOpen => _webSocket.State == WebSocketState.Open;

  private readonly WebSocket _webSocket;
  private readonly Abstractions.IObserver<WebSocketMessage> _messageObserver;
  public IObserverListener<WebSocketMessage> MessageListener => _messageObserver;

  public AppWebSocket(Id id, WebSocket webSocket, Abstractions.IObserver<WebSocketMessage> messageObserver)
  {
    _id = id;
    _webSocket = webSocket;
    _messageObserver = messageObserver;

    if (webSocket.State != WebSocketState.Open)
    {
      throw new Exception("WebSocket is not open");
    }
  }

  public Task RunAsync() =>
    _webSocket.OnMessage(_messageObserver.Notify, CancellationToken.None);

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
}