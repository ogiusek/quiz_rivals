using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Common.Extensions;
using Users.DAL.Builder;
using Users.App.Builder;
using Users.Api.Builder;
using Users.Configuration;

namespace Users.Builder;

public static class AddUsersModuleExt
{
  public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
  {
    IConfiguration section = configuration.GetSection(UsersOptions.SectionName);
    UsersOptions options = section.Get<UsersOptions>() ?? new UsersOptions();

    options.Validate().Throw();

    services
      .Configure<UsersOptions>(section)
      .AddSingleton(e => new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret)),
        SecurityAlgorithms.HmacSha256
      ))
      .AddAuthentication(options.BearerScheme)
      .AddJwtBearer(o =>
      {
        o.Audience = options.Audience;
        o.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateLifetime = true,
          ValidIssuer = options.Issuer,
          ClockSkew = TimeSpan.Zero,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret))
        };
      });

    services
      .AddUsersDAL(configuration)
      .AddUsersApp()
      .AddUsersApi();

    return services;
  }
}