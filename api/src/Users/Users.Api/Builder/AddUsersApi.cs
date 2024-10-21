using Common.Api.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Users.App.Builder;

namespace Users.Api.Builder;

public static class AddUsersApiExt
{
  public static IServiceCollection AddUsersApi(this IServiceCollection services)
  {
    services
      .AddControllers()
      .AddApplicationPart(typeof(AddUsersApiExt).Assembly);
    return services;
  }
}