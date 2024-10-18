using Common.App.Adapters;
using Common.App.AdaptersImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Common.App.Builder;

public static class CommonAppBuilder
{
  public static IServiceCollection AddWebSocketStorageAdapter(this IServiceCollection services)
  {
    services
      .AddSingleton<IWebSocketsStorage, WebSocketsStorage>();
    return services;
  }
}