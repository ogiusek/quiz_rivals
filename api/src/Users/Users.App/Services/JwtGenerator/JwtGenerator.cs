using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Abstractions;
using FileSaver.Adapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users.App.Abstractions;
using Users.App.Services.JwtStorage;
using Users.Configuration;
using Users.Core.Models;

namespace Users.App.Services.JwtGenerator;

#nullable enable
internal class JwtGenerator : IJwtGenerator
{
  private readonly SigningCredentials _signingCredentials;
  private readonly JwtSecurityTokenHandler _jwtSecurityToken;
  private readonly UsersOptions _options;
  private readonly IClock _clock;
  private readonly IFileApi _fileApi;

  public JwtGenerator(SigningCredentials signingCredentials, IConfiguration configuration, IClock clock, IFileApi fileApi)
  {
    _signingCredentials = signingCredentials;
    _jwtSecurityToken = new();
    _options = configuration.GetSection(UsersOptions.SectionName).Get<UsersOptions>() ?? new UsersOptions();
    _clock = clock;
    _fileApi = fileApi;
  }

  public Jwt Generate(JwtPayload payload)
  {
    var claims = new List<Claim>
    {
      new(SessionClaims.Id, payload.user.Id.Value),
      new(SessionClaims.Nick, payload.user.Nick.Value),
      new(SessionClaims.Photo, _fileApi.GetAddress(payload.user.PhotoPath).Address),
    };

    DateTime now = _clock.Now;
    DateTime expiration = payload.user.PasswordHash is null ? now.AddYears(1) : now.AddHours(6);

    var token = new JwtSecurityToken
    (
      _options.Issuer,
      _options.Audience,
      claims,
      now,
      expiration,
      _signingCredentials
    );

    var tokenString = _jwtSecurityToken.WriteToken(token);

    return new Jwt(tokenString);
  }
}