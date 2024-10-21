using Common.ValueObjects;

namespace Common.DAL.Tests.Mocks.Entities;

internal sealed class RelationlessEntity
{
  public Id Id
  {
    get;
    init;
  }

  public string Name
  {
    get;
    init;
  }
  public RelationlessEntity WithName(string name) => new(this) { Name = name };

  public string? Description
  {
    get;
    init;
  }
  public RelationlessEntity WithDescription(string? description) => new(this) { Description = description };

  private RelationlessEntity(RelationlessEntity dope)
  {
    Id = dope.Id;
    Name = dope.Name;
    Description = dope.Description;
  }

  public RelationlessEntity(Id id, string name)
  {
    Id = id;
    Name = name;
  }
}