namespace Common.Core.Exceptions;

public abstract class CoreException : Exception
{
  public CoreException(string message) : base(message)
  {
  }
}