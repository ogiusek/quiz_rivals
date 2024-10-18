using Common.ValueObjects;

namespace FileSaver.Adapter.ValueObjects;

public sealed class FilePath
{
  public string Path { get; }
  public string Extension => Path.Substring(Path.LastIndexOf(".") + 1).ToLower();

  public FilePath(string path)
  {
    Path = path;
    if (path.StartsWith("/") || path.EndsWith("/"))
    {
      throw new ArgumentException("File path must not start with '/' and must not end with '/'");
    }

    string file = path.Substring(path.LastIndexOf("/"));
    if (!file.Contains("."))
    {
      throw new ArgumentException("File path must contain extension");
    }
  }
}