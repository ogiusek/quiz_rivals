using FileSaver.Adapter.ValueObjects;

namespace FileSaver.Adapter.ValueObjects.Types;

public class FileContentTypeTests
{
  public void GetContentType_Should_Return_Existing_ContentType()
  {
    // arrange
    FileExtension extension = FileExtension.PNG;

    // act
    FileContentType contentType = FileContentType.GetContentType(extension);

    // assert
    Assert.Equal(FileContentType.PNG.ContentType, contentType.ContentType);
  }

  public void GetContentType_Should_Return_Default_ContentType()
  {
    // arrange
    FileExtension extension = new(".ext");

    // act
    FileContentType contentType = FileContentType.GetContentType(extension);

    // assert
    Assert.Equal(FileContentType.DEFAULT.ContentType, contentType.ContentType);
  }
}