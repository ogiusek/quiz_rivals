using FileSaver.Adapter.Types;

namespace FileSaver.Adapter.Tests.Types;

public class SavableFileStreamTests
{
  #region Arrange
  Stream _stream;
  FileExtension _extension;

  public SavableFileStreamTests()
  {
    _stream = new MemoryStream();
    _extension = FileExtension.PNG;
  }
  #endregion Arrange

  [Fact]
  public void Created_Should_Have_Constructor_Parameters()
  {
    // arrange
    SavableFileStream file = new SavableFileStream(_stream, _extension);

    // assert
    Assert.Equal(_stream, file.Stream);
    Assert.Equal(_extension, file.Extension);
  }
}