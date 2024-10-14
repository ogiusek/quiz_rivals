namespace Common.Exceptions;

public class GoneException : CustomException
{
  public GoneException(string message) : base(410, message) { }
}