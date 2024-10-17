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
using Serilog;
using Serilog.Events;
using Users.Builder;

namespace App;

public static class Program
{
  public static void Main(string[] args)
  {
    // create add builder and host it
    var builder = WebApplication.CreateBuilder(args);

    LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
    Log.Logger = loggerConfiguration.CreateLogger();

    builder.Host.UseSerilog();

    builder.Services
      .AddTransient(typeof(IHostEnvironment), e => builder.Environment);

    builder.Services
      .AddCommonApp()
      .AddCommonApi();

    builder.Services
      .AddUsersModule(builder.Configuration);

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
}
