using Microsoft.AspNetCore.Mvc;

namespace Common.Api.ActionResults;

public class ActionResultWithCallback(IActionResult result, Func<Task> onFinish) : ActionResult
{
  public override async Task ExecuteResultAsync(ActionContext context)
  {
    context.HttpContext.Response.OnCompleted(onFinish);
    await result.ExecuteResultAsync(context);
  }
}