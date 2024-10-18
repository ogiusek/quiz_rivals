using System.Reflection;

namespace FileSaver.Adapter.Types;

public class FileContentType
{
  public string ContentType { get; }

  public FileContentType(string contentType)
  {
    ContentType = contentType;
  }

  public static FileContentType GetContentType(FileExtension extension)
  {
    PropertyInfo? property = typeof(FileExtension).GetProperty(extension.Extension.ToUpper(), BindingFlags.Public | BindingFlags.Static);
    if (property is null)
    {
      return DEFAULT;
    }
    FileContentType? value = (FileContentType?)property.GetValue(null);
    return value is null ?
      DEFAULT :
      value;
  }

  public static FileContentType DEFAULT => new("application/octet-stream");
  public static FileContentType PNG => new("image/png");
  public static FileContentType JPG => new("image/jpeg");
  public static FileContentType JPEG => new("image/jpeg");
  public static FileContentType SVG => new("image/svg+xml");
  public static FileContentType WEBP => new("image/webp");

  public static FileContentType TXT => new("text/plain");
}