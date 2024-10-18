using Common.Types;
using FileSaver.Adapter.Implementations;
using FileSaver.Adapter.Types;
using Microsoft.Extensions.Configuration;

namespace FileSaver.Adapter.Tests.Implementations;

public class FileStorageTests
{
  #region Arrange
  IFileStorage _fileStorage;
  string _projectDirectory;
  string _relativePath;

  SavableFileStream savableStream;

  public FileStorageTests()
  {
    // get project directory
    string currentDirectory = Directory.GetCurrentDirectory();
    _projectDirectory = currentDirectory.Substring(0, currentDirectory.LastIndexOf("/src", StringComparison.Ordinal));
    _relativePath = "/test_storage";

    var configurationDictionary = new Dictionary<string, string?>() {
      { "App:ProjectDirectory", _projectDirectory },
      { "FileStorage:RelativePath", _relativePath }
    };

    IConfiguration configuration = new ConfigurationBuilder()
      .AddInMemoryCollection(configurationDictionary)
      .Build();
    _fileStorage = new FileStorage(configuration);

    savableStream = new SavableFileStream(
      new MemoryStream([1, 2, 3]),
      FileExtension.TXT
    );
  }
  #endregion

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  public async Task SaveFile_Should_Save_File(int repeats)
  {
    // act
    IEnumerable<Res<FilePath>> filePaths = await Task.WhenAll(Enumerable
      .Range(0, repeats)
      .Select(async _ => await _fileStorage.Save(savableStream)));

    IEnumerable<string> fullPaths = filePaths
      .Select(filePath => $"{_projectDirectory}{_relativePath}/{filePath.Value?.Path}");

    // assert
    Assert.All(filePaths, filePath => Assert.True(filePath.IsSuccess));
    Assert.All(fullPaths, fullPath => Assert.True(File.Exists(fullPath)));

    // cleanup
    await Task.WhenAll(filePaths.Select(filePath => _fileStorage.Remove(filePath.Value!)));
    Assert.All(fullPaths, fullPath => Assert.False(File.Exists(fullPath)));
  }
}