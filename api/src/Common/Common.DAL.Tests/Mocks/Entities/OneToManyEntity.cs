using Common.ValueObjects;

namespace Common.DAL.Tests.Mocks.Entities;

public class OneToManyManyEntity
{
  public required Id Id { get; init; }
  public required Id OtherEntityId { get; init; }
  public OneToManyOneEntity? OtherEntity { get; init; }
  public OneToManyManyEntity WithEntity(OneToManyOneEntity entity) => new()
  {
    Id = Id,
    OtherEntityId = OtherEntityId,
    OtherEntity = entity
  };
}

public class OneToManyOneEntity
{
  public required Id Id { get; init; }
  public IEnumerable<OneToManyManyEntity> Entities { get; set; } = new List<OneToManyManyEntity>();
  public OneToManyOneEntity WithEntity(OneToManyManyEntity entity) => new()
  {
    Id = Id,
    Entities = Entities.Append(entity)
  };

  public OneToManyOneEntity WithEntities(IEnumerable<OneToManyManyEntity> entities) => new()
  {
    Id = Id,
    Entities = Entities.Concat(entities)
  };
}