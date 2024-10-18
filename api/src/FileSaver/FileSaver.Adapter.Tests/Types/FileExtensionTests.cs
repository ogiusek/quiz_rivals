using FileSaver.Adapter.Types;

namespace FileSaver.Adapter.Tests.Types;

public class FileExtensionTests
{
  [Fact]
  public void Exists_Should_Return_True_For_Existing_Extension()
  {
    // arrange
    FileExtension extension = FileExtension.PNG;

    // act
    bool result = FileExtension.Exists(extension);

    // assert
    Assert.True(result);
  }

  [Fact]
  public void Exists_Should_Return_False_For_Non_Existing_Extension()
  {
    // arrange
    FileExtension extension = new("ext");

    // act
    bool result = FileExtension.Exists(extension);

    // assert
    Assert.False(result);
  }

  [Fact]
  public void GetExtension_Should_Return_Extension_For_Existing_Extension()
  {
    // arrange
    FileContentType contentType = FileContentType.PNG;

    // act
    FileExtension? result = FileExtension.GetExtension(contentType);

    // assert
    Assert.Equal(FileExtension.PNG.Extension, result?.Extension);
  }

  [Fact]
  public void GetExtension_Should_Return_Null_For_Non_Existing_Extension()
  {
    // arrange
    FileContentType contentType = new("stream/ext");

    // act
    FileExtension? result = FileExtension.GetExtension(contentType);

    // assert
    Assert.Null(result);
  }
}