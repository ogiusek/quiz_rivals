using FileSaver.Adapter.ValueObjects;

namespace FileSaver.Adapter.ValueObjects.Types;

public class FilePathTests
{
  [Fact]
  public void Created_Should_Have_Constructor_Parameters()
  {
    // arrange
    string path = "directory/file.ext";
    FilePath filePath = new(path);

    // assert
    Assert.Equal(path, filePath.Path);
  }

  [Fact]
  public void Created_With_Slash_On_Start_Should_Fail()
  {
    // arrange
    string path = "/directory/file.ext";
    Assert.Throws<ArgumentException>(() => new FilePath(path));
  }

  [Fact]
  public void Created_With_Slash_On_End_Should_Fail()
  {
    // arrange
    string path = "directory/file.ext/";
    Assert.Throws<ArgumentException>(() => new FilePath(path));
  }
}