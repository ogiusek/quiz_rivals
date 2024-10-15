namespace Common.Exceptions;

public class ConflictException : CustomException
{
  public ConflictException(string message) : base(409, message) { }
}