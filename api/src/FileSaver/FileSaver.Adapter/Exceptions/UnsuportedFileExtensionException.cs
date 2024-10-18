using FileSaver.Adapter.Types;

namespace FileSaver.Adapter.Exceptions;

public sealed class UnsuportedFileExtensionException : FileSaverException
{
  public UnsuportedFileExtensionException(FileExtension extension) : base($"Unsuported file extension .{extension.Extension}")
  { }
}