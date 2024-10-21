using System.Text.Json;
using Common.Adapters;
using Common.DAL.Tests.Mocks;
using Common.DAL.Tests.Mocks.Entities;
using Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Common.DAL.Tests.MockEntitiesTests;

public class OneToManyEntityTests
{
  #region Arrange

  DbContextMock _dbContext;
  IRepository<OneToManyManyEntity> _manyEntityRepository;
  IRepository<OneToManyOneEntity> _oneEntityRepository;

  IEnumerable<OneToManyManyEntity> _manyEntities;
  OneToManyOneEntity _oneEntity;

  public OneToManyEntityTests()
  {
    DbContextOptionsBuilder<DbContextMock> optionsBuilder = new DbContextOptionsBuilder<DbContextMock>();
    optionsBuilder
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    _dbContext = new DbContextMock(optionsBuilder.Options);
    _manyEntityRepository = new RepositoryMock(_dbContext);
    _oneEntityRepository = new RepositoryMock(_dbContext);
    _dbContext.Database.EnsureCreated();

    _oneEntity = new OneToManyOneEntity
    {
      Id = Id.New()
    };

    _manyEntities = [
      new OneToManyManyEntity{ Id = Id.New(), OtherEntityId = _oneEntity.Id },
      new OneToManyManyEntity{ Id = Id.New(), OtherEntityId = _oneEntity.Id }
    ];
  }

  internal void Dispose()
  {
    _dbContext.Database.EnsureDeleted();
  }

  #endregion

  [Fact]
  public async Task Select_One_Should_Return_One_With_Many()
  {
    // arrange
    await _oneEntityRepository.Add(_oneEntity);
    await _manyEntityRepository.Add(_manyEntities);

    // act
    IEnumerable<OneToManyOneEntity> entities = await _oneEntityRepository.Get.ToListAsync();
    OneToManyOneEntity? entity = entities.FirstOrDefault();

    // assert
    Assert.Single(entities);
    Assert.NotNull(entity);
    Assert.Equal(entity.Id.Value, entity.Id.Value);
    Assert.Equal(2, entity.Entities.Count());
    Assert.Contains(entity.Entities, e => e.Id.Value == _manyEntities.First().Id.Value);
    Assert.Contains(entity.Entities, e => e.Id.Value == _manyEntities.Last().Id.Value);
    Assert.DoesNotContain(entity.Entities, e => e.OtherEntity is not null);
  }

  [Fact]
  public async Task Select_Many_Should_Return_Many_With_One()
  {
    // arrange
    await _oneEntityRepository.Add(_oneEntity);
    await _manyEntityRepository.Add(_manyEntities);

    // act
    IEnumerable<OneToManyManyEntity> entities = await _manyEntityRepository.Get.ToListAsync();

    // assert
    Assert.Equal(2, entities.Count());
    Assert.Contains(entities, e => e.Id.Value == _manyEntities.First().Id.Value);
    Assert.Contains(entities, e => e.Id.Value == _manyEntities.Last().Id.Value);
    Assert.DoesNotContain(entities, e => e.OtherEntity is null);
  }
}