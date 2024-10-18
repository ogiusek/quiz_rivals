namespace Common.App.Types;

public class FileExtension
{
  public string Extension { get; }

  public FileExtension(string extension)
  {
    Extension = extension.ToLower();
  }

  public static FileExtension PNG => new("png");
  public static FileExtension JPG => new("jpg");
  public static FileExtension JPEG => new("jpeg");
  public static FileExtension SVG => new("svg");
  public static FileExtension WEBP => new("webp");

  public static FileExtension TXT => new("txt");
}