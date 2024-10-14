using Common.Abstractions;
using Common.App.Adapters;
using Common.Types;

namespace Common.App.AdaptersImplementations;

public sealed class WebSocketsStorage : IWebSocketsStorage
{
#nullable enable
  private readonly Dictionary<Id, IAppWebSocket> _webSockets = new();

  public Task<Res> Add(IAppWebSocket webSocket)
  {
    Res res = Res.Fail(new Exception($"Web socket with id {webSocket.Id} already exists"));
    if (!_webSockets.ContainsKey(webSocket.Id))
    {
      _webSockets.Add(webSocket.Id, webSocket);
      res = Res.Success();
    }
    return Task.FromResult(res);
  }

  public Task<IAppWebSocket?> Get(Id id)
  {
    IAppWebSocket? webSocket = _webSockets.TryGetValue(id, out var value) ? value : null;
    return Task.FromResult(webSocket);
  }

  public Task<Res> Remove(Id id)
  {
    Res res = Res.Fail(new Exception($"Web socket with id {id} does not exist"));
    if (_webSockets.ContainsKey(id))
    {
      _webSockets.Remove(id);
      res = Res.Success();
    }
    return Task.FromResult(res);
  }
#nullable restore
}