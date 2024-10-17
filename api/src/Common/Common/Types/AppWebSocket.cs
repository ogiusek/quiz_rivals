using System.Net.WebSockets;
using Common.Abstractions;
using Common.AbstractionsImplementations;
using Common.Extensions;
using Serilog;

namespace Common.Types;

public sealed class AppWebSocket : IAppWebSocket
{
  private Id _id;
  public Id Id => _id;
  public bool IsOpen => _webSocket.State == WebSocketState.Open;

  private readonly WebSocket _webSocket;
  private readonly Abstractions.IObserver<WebSocketMessage> _messageObserver;
  public IObserverListener<WebSocketMessage> OnMessage => _messageObserver;

  private readonly IObserver _closeObserver;
  public IObserverListener OnClose => _closeObserver;

  private readonly IObserver _openObserver;
  public IObserverListener OnOpen => _openObserver;

  public AppWebSocket(Id id, WebSocket webSocket, Abstractions.IObserver<WebSocketMessage> messageObserver, IObserver closeObserver, IObserver openObserver)
  {
    _id = id;
    _webSocket = webSocket;
    _messageObserver = messageObserver;
    _closeObserver = closeObserver;
    _openObserver = openObserver;

    if (webSocket.State != WebSocketState.Open)
    {
      throw new Exception("WebSocket is not open");
    }

    if (_messageObserver is null)
    {
      throw new ArgumentNullException(nameof(messageObserver));
    }

    if (_closeObserver is null)
    {
      throw new ArgumentNullException(nameof(closeObserver));
    }

    if (_openObserver is null)
    {
      throw new ArgumentNullException(nameof(openObserver));
    }
  }

  public async Task RunAsync(CancellationToken cancellationToken = default)
  {
    await _openObserver.Notify();
    try
    {
      await _webSocket.OnMessage(_messageObserver.Notify, cancellationToken);
    }
    catch (OperationCanceledException)
    {
    }

    await Close();
    await _closeObserver.Notify();
  }

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