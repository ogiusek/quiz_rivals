namespace Users.App.Services.JwtStorage;

#nullable enable
public interface IJwtGetter
{
  Jwt? Get();
}
#nullable restore