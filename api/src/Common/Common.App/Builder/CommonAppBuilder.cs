using System.Runtime.InteropServices;
using Common.App.Adapters;
using Common.App.AdaptersImplementations;
using Common.App.Types;
using Common.Types;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Common.App.Builder;

public static class CommonAppBuilder
{
  public static IServiceCollection AddWebSocketStorageAdapter(this IServiceCollection services)
  {
    services
      .AddSingleton<IWebSocketsStorage, WebSocketsStorage>();
    return services;
  }

  public static IServiceCollection AddFileStorageAdapter(this IServiceCollection services)
  {
    services
      .AddScoped<IFileApi, FileApi>()
      .AddScoped<IFileStorage, FileStorage>();
    return services;
  }
}