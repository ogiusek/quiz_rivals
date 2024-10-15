using System.Reflection;
using System.Text;
using Common.Abstractions;
using Common.AbstractionsImplementations;
using Common.Api.Abstractions;
using Common.Api.Builder;
using Common.App.Adapters;
using Common.App.AdaptersImplementations;
using Common.App.Builder;
using Common.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App;

public static class Program
{
  public static void Main(string[] args)
  {
    // create add builder and host it
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
      .AddTransient(typeof(IHostEnvironment), e => builder.Environment);

    builder.Services
      .AddCommonApp()
      .AddCommonApi();

    builder.Services.AddControllers();

    builder.Services.AddCors(options =>
      options.AddDefaultPolicy(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
      )
    );

    var app = builder.Build();
    app.UseWebSockets();
    app.UseCors();
    app.MapControllers();

    app.Run();
  }

  // public static void AddSubscribers(IAppWebSocket appWebSocket)
  // {
  //   EventListener<WebSocketMessage> messageListener = new EventListener<WebSocketMessage>(Id.New(), async (message) =>
  //   {
  //     await appWebSocket.Send(new WebSocketMessage(message.Message));
  //   });

  //   appWebSocket.MessageListener.Subscribe(messageListener);
  // }
}
