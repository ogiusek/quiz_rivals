using Common.Adapters;
using Common.App.Abstractions;
using Common.Types;
using Microsoft.EntityFrameworkCore;
using Users.App.Exceptions;
using Users.Core.Models;

namespace Users.App.Commands.SetEmail;

internal sealed class SetEmailHandler(
  IRepository<User> _usersRepository
) : ICustomCommandHandler<SetEmailCommand>
{
#nullable enable
  bool ICustomCommandHandler<SetEmailCommand>.RunAfterExecute { get; set; } = false;

  async Task<Res> ICustomCommandHandler<SetEmailCommand>.Execute(SetEmailCommand command)
  {
    User? user = await _usersRepository.Get.AsAsyncEnumerable().Where(u => u.Id.Value == command.Id).SingleOrDefaultAsync();
    if (user is null)
    {
      return Res.Fail(new UserNotFound());
    }
    user = user.WithEmail(new(command.Email));
    Res updated = await _usersRepository.Update(user);
    return updated;
  }
#nullable restore
}
