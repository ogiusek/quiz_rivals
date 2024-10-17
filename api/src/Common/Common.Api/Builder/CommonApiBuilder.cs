using System.Reflection;
using Common.AbstractionsImplementations;
using Common.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Builder;

public static class CommonApiBuilder
{
  public static IServiceCollection AddCommonApi(this IServiceCollection services)
  {
    services
      .AddTransient<Common.Abstractions.IObserver<WebSocketMessage>, Observer<WebSocketMessage>>()
      .AddTransient<Common.Abstractions.IObserver, Observer>();

    Assembly assembly = typeof(CommonApiBuilder).Assembly;
    services
      .AddControllers()
      .AddApplicationPart(assembly);

    return services;
  }
}