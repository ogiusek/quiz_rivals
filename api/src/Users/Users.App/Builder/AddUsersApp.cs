using Common.App.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Users.App.Services.JwtGenerator;
using Users.App.Services.JwtStorage;
using Users.App.Commands.RegisterGuest;
using Users.App.Commands;
using Users.App.Commands.Register;
using Users.App.Commands.Login;
using Users.App.Queries.Profile;
using Users.App.Commands.SetNick;
using Users.App.Commands.SetEmail;
using Users.App.Commands.SetPassword;
using Users.App.Commands.SetPhoto;

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
      .AddScoped<ICustomCommandHandler<LoginCommand>, LoginHandler>()
      .AddScoped<ICustomCommandHandler<SetEmailCommand>, SetEmailHandler>()
      .AddScoped<ICustomCommandHandler<SetNickCommand>, SetNickHandler>()
      .AddScoped<ICustomCommandHandler<SetPasswordCommand>, SetPasswordHandler>()
      .AddScoped<ICustomCommandHandler<SetPhotoCommand>, SetPhotoHandler>();

    services
      .AddScoped<ICustomQueryHandler<ProfileQuery, ProfileQueryResponse>, ProfileQueryHandler>();
    return services;
  }
}