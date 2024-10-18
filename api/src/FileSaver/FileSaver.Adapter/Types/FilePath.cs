namespace FileSaver.Adapter.Types;

public sealed class FilePath
{
  public string Path { get; }

  public FilePath(string path)
  {
    Path = path;
    if (path.StartsWith("/") || path.EndsWith("/"))
      throw new ArgumentException("File path must not start with '/' and must not end with '/'");
  }
}