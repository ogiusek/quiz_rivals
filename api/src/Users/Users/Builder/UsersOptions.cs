using System.Text.Json.Serialization;
using Common.Exceptions;
using Common.Methods;
using Common.Types;

namespace Users.Builder;

internal record UsersOptions(
  [property: JsonPropertyName("BearerScheme")] string BearerScheme,
  [property: JsonPropertyName("Secret")] string Secret, // node -e "console.log(require('crypto').randomBytes(32).toString('hex'))"
  [property: JsonPropertyName("Audience")] string Audience,
  [property: JsonPropertyName("Issuer")] string Issuer,
  [property: JsonPropertyName("ExpirationTime")] TimeSpan ExpirationTime
)
{
  public static string SectionName = "Users";
  public UsersOptions() : this("Bearer", "", "", "", TimeSpan.Zero) { }

  public Res Validate()
  {
    Res res = Res.Success();

    string BearerSchemeName = JsonHelper.PropertyName<UsersOptions>(nameof(BearerScheme));
    if (string.IsNullOrEmpty(BearerScheme))
      res.Exceptions = res.Exceptions.Append(new ConfigurationException(SectionName, $"`{BearerSchemeName}` is required")
      );

    string SecretName = JsonHelper.PropertyName<UsersOptions>(nameof(Secret));
    if (string.IsNullOrEmpty(Secret))
      res.Exceptions = res.Exceptions.Append(new ConfigurationException(SectionName, $"`{SecretName}` is required"));

    string AudienceName = JsonHelper.PropertyName<UsersOptions>(nameof(Audience));
    if (string.IsNullOrEmpty(Audience))
      res.Exceptions = res.Exceptions.Append(new ConfigurationException(SectionName, $"`{AudienceName}` is required"));

    string IssuerName = JsonHelper.PropertyName<UsersOptions>(nameof(Issuer));
    if (string.IsNullOrEmpty(Issuer))
      res.Exceptions = res.Exceptions.Append(new ConfigurationException(SectionName, $"`{IssuerName}` is required"));

    string ExpirationTimeName = JsonHelper.PropertyName<UsersOptions>(nameof(ExpirationTime));
    if (string.IsNullOrEmpty(Secret))
      res.Exceptions = res.Exceptions.Append(new ConfigurationException(SectionName, $"`{ExpirationTimeName}` is required"));

    return res;
  }
};