using Common.Adapters;
using Common.DAL.Tests.Mocks;
using Common.DAL.Tests.Mocks.Entities;
using Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DAL.Tests.MockEntitiesTests;

public class RelationlessEntityTests
{
  #region Arrange

  DbContextMock _dbContext;
  IRepository<RelationlessEntity> _relationlessEntityRepository;

  public RelationlessEntityTests()
  {
    DbContextOptionsBuilder<DbContextMock> optionsBuilder = new DbContextOptionsBuilder<DbContextMock>();
    optionsBuilder
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    _dbContext = new DbContextMock(optionsBuilder.Options);
    _relationlessEntityRepository = new RepositoryMock(_dbContext);
    _dbContext.Database.EnsureCreated();
  }

  internal void Dispose()
  {
    _dbContext.Database.EnsureDeleted();
  }

  #endregion

  [Fact]
  public async Task Add_Test()
  {
    var entity = new RelationlessEntity
    (
      Id.New(),
      Guid.NewGuid().ToString()
    );

    await _relationlessEntityRepository.Add(entity);

    var entities = await _relationlessEntityRepository.Get.Where(x => x.Id.Value == entity.Id.Value).ToListAsync();
    RelationlessEntity? found = entities.FirstOrDefault();

    Assert.NotNull(found);
    Assert.Equal(entity.Id.Value, found.Id.Value);
    Assert.Equal(entity.Name, found.Name);
  }

  [Fact]
  public async Task Remove_Test()
  {
    var entity = new RelationlessEntity
    (
      Id.New(),
      Guid.NewGuid().ToString()
    );

    await _relationlessEntityRepository.Add(entity);
    await _relationlessEntityRepository.Remove(entity);

    var entities = await _relationlessEntityRepository.Get.Where(x => x.Id.Value == entity.Id.Value).ToListAsync();
    RelationlessEntity? found = entities.FirstOrDefault();

    Assert.Null(found);
  }

  [Fact]
  public async Task Update_Test()
  {
    // arrange
    var entity = new RelationlessEntity
    (
      Id.New(),
      Guid.NewGuid().ToString()
    );

    // act
    await _relationlessEntityRepository.Add(entity);
    var entityFetched = await _relationlessEntityRepository.Get.FirstOrDefaultAsync(x => x.Id.Value == entity.Id.Value);

    var edited = entity.WithName(Guid.NewGuid().ToString());
    await _relationlessEntityRepository.Update(edited);

    var editedFetched = await _relationlessEntityRepository.Get.FirstOrDefaultAsync(x => x.Id.Value == edited.Id.Value);

    await _relationlessEntityRepository.Remove(edited);
    var removedFetched = await _relationlessEntityRepository.Get.FirstOrDefaultAsync(x => x.Id.Value == edited.Id.Value);

    // assert
    Assert.NotNull(entityFetched);
    Assert.Equal(entity.Id.Value, entityFetched.Id.Value);
    Assert.Equal(entity.Name, entityFetched.Name);
    Assert.Equal(entity.Description, entityFetched.Description);

    Assert.NotNull(editedFetched);
    Assert.Equal(edited.Id.Value, editedFetched.Id.Value);
    Assert.Equal(edited.Name, editedFetched.Name);
    Assert.Equal(edited.Description, editedFetched.Description);

    Assert.Null(removedFetched);
  }
}