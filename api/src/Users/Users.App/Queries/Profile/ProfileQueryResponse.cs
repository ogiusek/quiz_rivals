using Common.App.Abstractions;

namespace Users.App.Queries.Profile;

#nullable enable
public record ProfileQueryResponse
(
  string Id,
  string Nick,
  string Photo,
  string? Email,
  DateTime CreatedAt
) : ICustomQueryResponse;
#nullable restore