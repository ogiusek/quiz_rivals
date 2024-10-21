using Common.Adapters;
using Common.Types;
using Microsoft.EntityFrameworkCore;
using Users.Core.Models;

namespace Users.DAL.Models.UsersModel;

internal sealed class UserRepository : IEfCoreRepository<User>
{
  private readonly UsersDbContext _dbContext;
  DbContext IEfCoreRepository<User>._dbContext => _dbContext;

  public UserRepository(UsersDbContext dbContext)
  {
    _dbContext = dbContext;
  }
}