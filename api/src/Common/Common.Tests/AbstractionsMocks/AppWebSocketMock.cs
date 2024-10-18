using Common.Abstractions;
using Common.Types;

namespace Common.Tests.AbstractionsMocks;

public class AppWebSocketMock : IAppWebSocket
{
  Id _id;
  Id IAppWebSocket.Id => _id;

  bool _isOpen = true;
  bool IAppWebSocket.IsOpen => _isOpen;

  Common.Abstractions.IObserver<WebSocketMessage> _messageObserver;
  IObserverListener<WebSocketMessage> IAppWebSocket.OnMessage => _messageObserver;

  Common.Abstractions.IObserver _openObserver;
  public IObserverListener OnOpen => _openObserver;

  Common.Abstractions.IObserver _closeObserver;
  public IObserverListener OnClose => _closeObserver;

  public AppWebSocketMock(Id id, Common.Abstractions.IObserver<WebSocketMessage> messageObserver, Common.Abstractions.IObserver closeObserver, Common.Abstractions.IObserver openObserver)
  {
    _id = id;
    _messageObserver = messageObserver;
    _openObserver = openObserver;
    _closeObserver = closeObserver;
  }

  public Task<Res> Close()
  {
    _isOpen = false;
    return Task.FromResult(Res.Success());
  }

  public Task<Res> Send(WebSocketMessage message)
  {
    return Task.FromResult(Res.Success());
  }
}