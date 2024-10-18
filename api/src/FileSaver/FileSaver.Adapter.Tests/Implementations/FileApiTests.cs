using FileSaver.Adapter.Implementations;
using FileSaver.Adapter.Types;
using Microsoft.Extensions.Configuration;

namespace FileSaver.Adapter.Tests.Implementations;

public class FileApiTests
{
  #region Arrange

  IFileApi _fileApi;
  FilePath _filePath;
  FileAddress _fileAddress;
  string _apiUrl;

  public FileApiTests()
  {
    _filePath = new FilePath("directory/file.ext");

    _fileAddress = new FileAddress("http://localhost/storage/directory/file.ext");

    _apiUrl = "http://localhost";

    IConfiguration configuration = new ConfigurationBuilder().Build();
    configuration["App:Address"] = _apiUrl;
    _fileApi = new FileApi(configuration);
  }
  #endregion

  public void GetAddress_Should_Return_Expected_Address()
  {
    // act
    FileAddress result = _fileApi.GetAddress(_filePath);

    // assert
    Assert.Equal(_fileAddress.Address, result.Address);
  }

  public void GetPath_Should_Return_Expected_Path()
  {
    // act
    FilePath result = _fileApi.GetPath(_fileAddress);

    // assert
    Assert.Equal(_filePath.Path, result.Path);
  }
}