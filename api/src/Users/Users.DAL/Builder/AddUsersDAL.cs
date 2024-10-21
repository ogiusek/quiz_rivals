using Common.Abstractions;
using Common.Adapters;
using Common.Extensions;
using Common.Types;
using Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Users.Core.Models;
using Users.DAL.Models.UsersModel;

namespace Users.DAL.Builder;

public static class AddUsersDALExt
{
  public static IServiceCollection AddUsersDAL(this IServiceCollection services, IConfiguration configuration)
  {
    services
      .AddDbContext<UsersDbContext>(options =>
      {
        options
          .UseNpgsql(configuration["App:ConnectionString"])
          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
      });

    services
      .AddScoped<UsersDbContext>()
      .AddScoped(IRepository<User>.Interfaces, typeof(UserRepository));

    return services;
  }
}