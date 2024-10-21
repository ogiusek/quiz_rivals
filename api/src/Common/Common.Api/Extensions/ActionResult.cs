using Common.Api.ActionResults;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Extensions;

public static class ActionResultExt
{
  public static ActionResult WithCallback(this ActionResult result, Func<Task> callback) =>
    new ActionResultWithCallback(result, callback);
}