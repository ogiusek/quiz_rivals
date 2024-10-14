namespace Common.Types;

public class Res
{
  public bool IsFailure => ExceptionsList.Any();
  public bool IsSuccess => !IsFailure;

  private List<Exception> ExceptionsList { get; set; } = new();
  public IEnumerable<Exception> Exceptions
  {
    get => ExceptionsList;
    set => ExceptionsList = value.ToList();
  }

  public Res() { }

  protected Res(IEnumerable<Exception> exceptions)
  { Exceptions = exceptions.ToList(); }

  public static Res Success() => new();
  public static Res Fail(Exception exception) => new([exception]);
  public static Res Fail(IEnumerable<Exception> exception) => new(exception);
}