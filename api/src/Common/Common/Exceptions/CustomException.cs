namespace Common.Exceptions;

public abstract class CustomException : Exception
{
  public new string Message { get; set; }
  public int Code { get; set; }

  protected CustomException(int code, string message)
  {
    Message = message;
    Code = code;
  }
}