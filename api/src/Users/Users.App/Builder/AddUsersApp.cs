using Common.App.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Users.App.Services.JwtGenerator;
using Users.App.Services.JwtStorage;
using Users.App.Commands.RegisterGuest;
using Users.App.Commands;
using Users.App.Commands.Register;
using Users.App.Commands.Login;

namespace Users.App.Builder;

public static class AddUsersAppExt
{
  public static IServiceCollection AddUsersApp(this IServiceCollection services)
  {
    services
      .AddScoped<JwtStorage, JwtStorage>()
      .AddScoped<IJwtGetter>(s => s.GetService<JwtStorage>())
      .AddScoped<IJwtSaver>(s => s.GetService<JwtStorage>())
      .AddSingleton<IJwtGenerator, JwtGenerator>();

    services
      .AddScoped<ICustomCommandHandler<RegisterGuestCommand>, RegisterGuestHandler>()
      .AddScoped<ICustomCommandHandler<RegisterCommand>, RegisterHandler>()
      .AddScoped<ICustomCommandHandler<LoginCommand>, LoginHandler>();
    return services;
  }
}