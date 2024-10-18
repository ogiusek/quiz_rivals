using System.Reflection;

namespace FileSaver.Adapter.Types;

public class FileExtension
{
  public string Extension { get; }

  public FileExtension(string extension)
  {
    Extension = extension.ToLower();
    Extension = Extension.StartsWith(".") ? Extension.Substring(1) : Extension;
  }

  public static FileExtension? GetExtension(FileContentType contentType)
  {
    IEnumerable<PropertyInfo> properties = typeof(FileExtension).GetProperties(BindingFlags.Public | BindingFlags.Static);
    PropertyInfo? property = properties.FirstOrDefault(p => contentType.ContentType.Substring(contentType.ContentType.IndexOf('/')).ToLower().Contains(p.Name.ToLower()));

    return property is null ?
      null :
      property.GetValue(null) as FileExtension;
  }

  public static bool Exists(FileExtension extension)
  {
    PropertyInfo? property = typeof(FileExtension).GetProperty(extension.Extension.ToUpper(), BindingFlags.Public | BindingFlags.Static);
    return property is not null;
  }

  public static FileExtension PNG => new("png");
  public static FileExtension JPG => new("jpg");
  public static FileExtension JPEG => new("jpeg");
  public static FileExtension SVG => new("svg");
  public static FileExtension WEBP => new("webp");

  public static FileExtension TXT => new("txt");
}