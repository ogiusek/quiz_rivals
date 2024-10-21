using Users.App.Services.JwtStorage;
using Users.Core.Models;

namespace Users.App.Services.JwtGenerator;

#nullable enable
public interface IJwtGenerator
{
  Jwt Generate(JwtPayload user);
}
#nullable restore