using Common.Adapters;
using Common.DAL.Tests.Mocks.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DAL.Tests.Mocks;

internal class RepositoryMock :
  IEfCoreRepository<RelationlessEntity>,
  IEfCoreRepository<OneToManyManyEntity>,
  IEfCoreRepository<OneToManyOneEntity>
{
  private DbContextMock _dbContext;

  public RepositoryMock(DbContextMock dbContext)
  {
    _dbContext = dbContext;
  }

  DbContext IEfCoreRepository<RelationlessEntity>._dbContext => _dbContext;
  DbContext IEfCoreRepository<OneToManyManyEntity>._dbContext => _dbContext;
  IQueryable<OneToManyManyEntity> IGetable<OneToManyManyEntity>.Get => _dbContext.Set<OneToManyOneEntity>()
    .Join(_dbContext.Set<OneToManyManyEntity>(),
      one => one.Id.Value,
      many => many.OtherEntityId.Value,
      (one, many) => many.WithEntity(one)
    ).AsQueryable();

  DbContext IEfCoreRepository<OneToManyOneEntity>._dbContext => _dbContext;
  IQueryable<OneToManyOneEntity> IGetable<OneToManyOneEntity>.Get => _dbContext.Set<OneToManyOneEntity>()
    .GroupJoin(_dbContext.Set<OneToManyManyEntity>(),
      one => one.Id.Value,
      many => many.OtherEntityId.Value,
      (one, many) => one.WithEntities(many)
    ).AsQueryable();

}