using Common.App.Abstractions;
using Users.Core.Models;

namespace Users.App.Queries.Profile;

public record ProfileQuery(string Id) : ICustomQuery<ProfileQueryResponse>;
