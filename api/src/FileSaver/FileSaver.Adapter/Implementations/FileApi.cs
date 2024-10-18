using Common.Exceptions;
using FileSaver.Adapter.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace FileSaver.Adapter.Implementations;

internal sealed class FileApi : IFileApi
{
  readonly string _storageUrl;

  public FileApi(IConfiguration configuration)
  {
    string? appAddress = configuration["App:Address"];

    if (string.IsNullOrEmpty(appAddress))
      throw new MissingConfigurationException("App:Address");

    if (appAddress.EndsWith("/"))
      throw new ConfigurationException("App:Address", "Address must not end with '/'");

    _storageUrl = $"{appAddress}/storage";
  }

  public FileAddress GetAddress(FilePath path)
    => new($"{_storageUrl}/{path.Path}");

  public FilePath GetPath(FileAddress address)
    => new(address.Address.Substring($"{_storageUrl}/".Length));
}