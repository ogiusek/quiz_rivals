using System.Numerics;
using System.Reflection;
using System.Text;
using Common.Api.Builder;
using Common.App.Builder;
using Common.Builder;
using FileSaver.Adapter.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Users.Builder;

namespace App;

public static class Program
{
  static void AddProjectPath(in IConfiguration configuration)
  {
    string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    string projectDirectory = assemblyPath.Substring(0, assemblyPath.LastIndexOf("/src", StringComparison.Ordinal));
    configuration["App:ProjectDirectory"] = projectDirectory;
  }

  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    AddProjectPath(builder.Configuration);

    LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
    Log.Logger = loggerConfiguration.CreateLogger();

    builder.Host.UseSerilog();

    builder.Services
      .AddTransient(typeof(IHostEnvironment), e => builder.Environment);

    builder.Services
      .AddWebSocketStorageAdapter()
      .AddFileSaver()
      .AddCommon()
      .AddCommonApiControllers();

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
