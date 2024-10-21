using Common.Exceptions;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Serilog;

namespace Common.Api.Extensions;

public static class ResExt
{
  private static void LogExceptions(IEnumerable<Exception> exceptions)
  {
    Log.Information("Exceptions: {@Exceptions}", exceptions);
  }

  public static ActionResult WithSuccess(this Res res, Func<ActionResult> actionResult)
  {
    if (res.IsSuccess)
    {
      return actionResult();
    }

    IEnumerable<(int, string)> errors = res.Exceptions.Select(ex => (
      ex switch
      {
        CustomException exception => (exception.Code, exception.Message),
        ArgumentException => (400, ex.Message),
        _ => (500, "Something went wrong")
      }
    ));

    if (errors.Any(e => e.Item1 == 500))
    {
      LogExceptions(res.Exceptions);
      return new ObjectResult("Something went wrong") { StatusCode = 500 };
    }

    if (errors.Count() == 1)
      return new ObjectResult(errors.Single().Item2)
      { StatusCode = errors.Single().Item1 };

    if (errors.All(e => e.Item1 == errors.First().Item1))
      return new ObjectResult(string.Join("\n", errors.Select(e => e.Item2)))
      { StatusCode = errors.First().Item1 };

    LogExceptions(res.Exceptions);
    return new ObjectResult("Something went wrong") { StatusCode = 500 };
  }
}