namespace Users.App.Services.JwtStorage;

#nullable enable
internal sealed class JwtStorage :
  IJwtGetter,
  IJwtSaver
{
  private Jwt? Token { get; set; }

  public void Save(Jwt token) => Token = token;
  public Jwt? Get() => Token;
}
#nullable restore