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

  public AppWebSocketMock(Id id, Common.Abstractions.IObserver<WebSocketMessage> messageObserver)
  {
    _id = id;
    _messageObserver = messageObserver;
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