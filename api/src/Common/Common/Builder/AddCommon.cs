using Common.Abstractions;
using Common.Abstractions.Hasher;
using Common.AbstractionsImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Builder;

public static class AddCommonExt
{
  public static IServiceCollection AddCommon(this IServiceCollection services)
  {
    services
      .AddSingleton<IClock, Clock>()
      .AddSingleton<IHasher, Hasher>();
    return services;
  }
}