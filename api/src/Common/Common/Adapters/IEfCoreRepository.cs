using System.Data.Common;
using Common.Types;
using Microsoft.EntityFrameworkCore;

namespace Common.Adapters;

public interface IEfCoreRepository<T> : IRepository<T>
  where T : class
{
  protected DbContext _dbContext { get; }

  IQueryable<T> IGetable<T>.Get => _dbContext.Set<T>();
  async Task<Res> IAddable<T>.Add(IEnumerable<T> models)
  {
    try
    {
      await _dbContext.Set<T>().AddRangeAsync(models);
      await _dbContext.SaveChangesAsync();
      return Res.Success();
    }
    catch (Exception ex)
    {
      return Res.Fail(ex);
    }
  }

  async Task<Res> IRemovable<T>.Remove(IEnumerable<T> models)
  {
    try
    {
      _dbContext.Set<T>().RemoveRange(models);
      await _dbContext.SaveChangesAsync();
      return Res.Success();
    }
    catch (Exception ex)
    {
      return Res.Fail(ex);
    }
  }

  async Task<Res> IUpdatable<T>.Update(IEnumerable<T> models)
  {
    try
    {
      _dbContext.ChangeTracker.Clear();
      _dbContext.Set<T>().UpdateRange(models);
      await _dbContext.SaveChangesAsync();
      return Res.Success();
    }
    catch (Exception ex)
    {
      return Res.Fail(ex);
    }
  }
}