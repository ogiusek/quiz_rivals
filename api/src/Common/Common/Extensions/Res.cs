using Common.Types;

namespace Common.Extensions;

public static class ResExt
{
  public static void Throw(this Res res)
  {
    if (res.IsSuccess)
    {
      return;
    }

    throw new AggregateException(res.Exceptions);
  }

  public static void Fail(this Res res, Exception exception)
  {
    res.Exceptions = res.Exceptions.Append(exception);
  }

  public static void Fail(this Res res, IEnumerable<Exception> exceptions)
  {
    res.Exceptions = res.Exceptions.Concat(exceptions);
  }
}