namespace Users.App.Services.JwtStorage;

#nullable enable
public interface IJwtSaver
{
  void Save(Jwt jwt);
}
#nullable restore