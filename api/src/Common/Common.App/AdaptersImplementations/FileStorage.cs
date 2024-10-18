using System.Security.Cryptography;
using Common.App.Adapters;
using Common.App.Types;
using Common.Exceptions;
using Common.Methods;
using Common.Types;
using Microsoft.Extensions.Configuration;

namespace Common.App.AdaptersImplementations;

internal sealed class FileStorage : IFileStorage
{
#nullable enable
  readonly string _storagePath;
  private SHA256 _sha256 => SHA256.Create();

  public FileStorage(IConfiguration configuration)
  {
    string? projectDirectory = configuration["App:ProjectDirectory"];
    string? relativePath = configuration["FileStorage:RelativePath"];

    if (string.IsNullOrEmpty(projectDirectory))
      throw new MissingConfigurationException("FileStorage:RelativePath");

    if (projectDirectory.EndsWith("/") || !projectDirectory.StartsWith("/"))
      throw new ConfigurationException("FileStorage:RelativePath", "Path must not end with '/' and must start with '/'");

    if (string.IsNullOrEmpty(relativePath))
      throw new MissingConfigurationException("FileStorage:RelativePath");

    if (relativePath.EndsWith("/") || !relativePath.StartsWith("/"))
      throw new ConfigurationException("FileStorage:RelativePath", "Path must not end with '/' and must start with '/'");

    _storagePath = $"{projectDirectory}{relativePath}";
  }

  private string GetFullPath(FilePath path) => $"{_storagePath}/{path.Path}";
  private FilePath GetFilePath(string path) => new(path.Substring($"{_storagePath}/".Length));
  private string GetTrackPath(FilePath path) => $"{GetFullPath(path)}.track.txt";

  private string GetContentHash(SavableFileStream file)
  {
    string hash = Convert.ToBase64String(_sha256.ComputeHash(file.Stream))
      .Replace("+", "_")
      .Replace("/", "-")
      .TrimEnd('=');
    file.Stream.Position = 0;
    return hash;
  }

  public async Task<Res> RemoveFile(FilePath path)
  {
    if (!File.Exists(GetFullPath(path)))
    {
      return Res.Fail(new FileNotFoundException(path.Path));
    }
    await UntrackFile(path); // File.Delete(filePath);
    return Res.Success();
  }

  public async Task<FilePath> CreateNewFile(SavableFileStream savableFile, string fileDirectory)
  {
    string fullPath = $"{fileDirectory}/{Guid.NewGuid()}.{savableFile.Extension.Extension}";

    using (FileStream file = File.Create(fullPath))
    {
      await savableFile.Stream.CopyToAsync(file);
      savableFile.Stream.Position = 0;
    }

    return GetFilePath(fullPath);
  }

  public async Task UntrackFile(FilePath path)
  {
    string fullPath = GetFullPath(path);
    string trackFile = GetTrackPath(path);

    int trackCount = Convert.ToInt32(File.ReadAllText(trackFile)) - 1;

    if (trackCount > 0)
    {
      await File.WriteAllTextAsync(trackFile, trackCount.ToString());
      return;
    }
    File.Delete(trackFile);
    File.Delete(fullPath);
    string directory = Path.GetDirectoryName(fullPath)!;
    while (Directory.GetFiles(directory).Length == 0)
    {
      Directory.Delete(directory);
      directory = Path.GetDirectoryName(directory)!;
    }
  }

  public async Task TrackFile(FilePath path)
  {
    string trackFile = GetTrackPath(path);
    string trackCount = File.Exists(trackFile) ?
      File.ReadAllText(trackFile) :
      "1";

    await File.WriteAllTextAsync(trackFile, trackCount);
  }

  public Task<FilePath?> FindFile(SavableFileStream stream, string fileDirectory)
  {
    if (!Directory.Exists(fileDirectory))
      return Task.FromResult<FilePath?>(null);

    var files = Directory.GetFiles(fileDirectory);

    if (files.Length == 0)
      return Task.FromResult<FilePath?>(null);

    foreach (var file in files)
    {
      using (var fileStream = File.OpenRead(file))
      {
        if (StreamHelper.StreamsMatch(fileStream, stream.Stream))
        {
          return Task.FromResult<FilePath?>(GetFilePath(file));
        }
      }
    }

    return Task.FromResult<FilePath?>(null);
  }

  public async Task<FilePath> SaveFile(SavableFileStream stream)
  {
    string hash = GetContentHash(stream);
    string fileDirectory = $"{_storagePath}/{stream.Extension.Extension}/{hash}";

    if (!Directory.Exists(fileDirectory))
    {
      Directory.CreateDirectory(fileDirectory);
      FilePath filePath = await CreateNewFile(stream, fileDirectory);
      await TrackFile(filePath);
      return filePath;
    }

    FilePath? existingFile = await FindFile(stream, fileDirectory);
    if (existingFile is not null)
    {
      await TrackFile(existingFile);
      return existingFile;
    }

    FilePath newFilePath = await CreateNewFile(stream, fileDirectory);
    await TrackFile(newFilePath);
    return newFilePath;
  }
#nullable restore
}