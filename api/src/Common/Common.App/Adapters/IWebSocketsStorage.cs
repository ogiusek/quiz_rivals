using Common.Abstractions;
using Common.Types;

namespace Common.App.Adapters;

public interface IWebSocketsStorage
{
#nullable enable
  public Task<IAppWebSocket?> Get(Id id);
  public Task<Res> Add(IAppWebSocket webSocket);
  public Task<Res> Remove(Id id);
#nullable restore
}