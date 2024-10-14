namespace Common.Exceptions;

public class ForbiddenException : CustomException
{
  public ForbiddenException(string message) : base(403, message) { }
}