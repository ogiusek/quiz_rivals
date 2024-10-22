using Common.Adapters;
using Common.App.Abstractions;
using Common.Types;
using FileSaver.Adapter;
using Microsoft.EntityFrameworkCore;
using Users.App.Exceptions;
using Users.Core.Models;

namespace Users.App.Queries.Profile;

#nullable enable
public class ProfileQueryHandler(
  IRepository<User> _usersRepository,
  IFileApi _fileApi
) : ICustomQueryHandler<ProfileQuery, ProfileQueryResponse>
{
  async Task<Res<ProfileQueryResponse>> ICustomQueryHandler<ProfileQuery, ProfileQueryResponse>.Execute(ProfileQuery query)
  {
    User? user = await _usersRepository.Get.AsAsyncEnumerable()
      .Where(u => u.Id.Value == query.Id)
      .SingleOrDefaultAsync();

    if (user is null)
    {
      return new Res<ProfileQueryResponse>(new UserNotFound());
    }

    ProfileQueryResponse response = new(
      user.Id.Value,
      user.Nick.Value,
      _fileApi.GetAddress(user.PhotoPath).Address,
      user.Email?.Value,
      user.CreatedAt
    );

    return new(response);
  }
}
#nullable restore