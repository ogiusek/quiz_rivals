namespace FileSaver.Adapter.Exceptions;

public abstract class FileSaverException : Exception
{
  public FileSaverException(string message) : base(message)
  { }
}