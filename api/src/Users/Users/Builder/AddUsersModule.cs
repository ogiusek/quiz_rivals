using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Users.Builder;

public static class AddUsersModuleExt
{
  public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
  {
    IConfiguration section = configuration.GetSection(UsersOptions.SectionName);
    UsersOptions options = section.Get<UsersOptions>() ?? new UsersOptions();

    var isConfigurationValid = options.Validate();
    if (isConfigurationValid.Exceptions.Any())
    {
      throw new AggregateException(isConfigurationValid.Exceptions);
    }

    services
      .Configure<UsersOptions>(section)
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

    return services;
  }
}