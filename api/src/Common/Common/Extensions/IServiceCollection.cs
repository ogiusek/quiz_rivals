using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions;

public static class IServiceCollectionExt
{
  public static IServiceCollection AddScoped(this IServiceCollection services, IEnumerable<Type> interfaces, Type implementation)
  {
    foreach (Type @interface in interfaces)
    {
      services.AddScoped(@interface, implementation);
    }
    return services;
  }
}