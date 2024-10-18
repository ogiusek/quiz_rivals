using Common.App.Adapters;
using Common.App.Types;
using Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Common.App.AdaptersImplementations;

internal sealed class FileApi : IFileApi
{
#nullable enable
  readonly string _apiUrl;

  public FileApi(IConfiguration configuration)
  {
    string? appAddress = configuration["App:Address"];

    if (string.IsNullOrEmpty(appAddress))
      throw new MissingConfigurationException("App:Address");

    if (appAddress.EndsWith("/"))
      throw new ConfigurationException("App:Address", "Address must not end with '/'");

    _apiUrl = $"{appAddress}/storage";
  }

  public FileAddress GetAddress(FilePath path)
    => new($"{_apiUrl}/{path.Path}");

  public FilePath GetPath(FileAddress address)
    => new(address.Address.Substring($"{_apiUrl}/".Length));
#nullable restore
}