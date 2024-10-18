using FileSaver.Adapter.Implementations;
using FileSaver.Adapter.Controller;
using Microsoft.Extensions.DependencyInjection;

namespace FileSaver.Adapter.Builder;

public static class FileSaverBuilder
{
  public static IServiceCollection AddFileSaver(this IServiceCollection services)
  {
    services
      .AddScoped<IFileApi, FileApi>()
      .AddScoped<IFileStorage, FileStorage>();

    services
      .AddControllers()
      .AddApplicationPart(typeof(StorageController).Assembly);

    return services;
  }
}