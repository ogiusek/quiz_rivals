using Common.Adapters;
using Common.App.Abstractions;
using Common.Types;
using Microsoft.EntityFrameworkCore;
using Users.App.Exceptions;
using Users.Core.Models;

namespace Users.App.Commands.SetNick;

internal sealed class SetNickHandler : ICustomCommandHandler<SetNickCommand>
{
#nullable enable
  bool ICustomCommandHandler<SetNickCommand>.RunAfterExecute { get; set; } = false;

  private readonly IRepository<User> _usersRepository;

  public SetNickHandler(IRepository<User> usersRepository)
  {
    _usersRepository = usersRepository;
  }

  async Task<Res> ICustomCommandHandler<SetNickCommand>.Execute(SetNickCommand command)
  {
    User? user = await _usersRepository.Get.AsAsyncEnumerable().Where(u => u.Id.Value == command.Id).SingleOrDefaultAsync();
    if (user is null)
    {
      return Res.Fail(new UserNotFound());
    }
    user = user.WithNick(new(command.Nick));
    Res updated = await _usersRepository.Update(user);
    return updated;
  }
#nullable restore
}