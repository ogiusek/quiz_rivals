using Common.Abstractions.Hasher;
using Common.Adapters;
using Common.App.Abstractions;
using Common.Types;
using Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Users.App.Exceptions;
using Users.Core.Models;

namespace Users.App.Commands.SetPassword;

internal sealed class SetPasswordHandler : ICustomCommandHandler<SetPasswordCommand>
{
  bool ICustomCommandHandler<SetPasswordCommand>.RunAfterExecute { get; set; } = false;

  private readonly IRepository<User> _usersRepository;
  private readonly IHasher _hasher;

  public SetPasswordHandler(IRepository<User> usersRepository, IHasher hasher)
  {
    _usersRepository = usersRepository;
    _hasher = hasher;
  }

  async Task<Res> ICustomCommandHandler<SetPasswordCommand>.Execute(SetPasswordCommand command)
  {
    User user = await _usersRepository.Get.AsAsyncEnumerable().Where(u => u.Id.Value == command.Id).SingleOrDefaultAsync();
    if (user is null)
    {
      return Res.Fail(new UserNotFound());
    }
    Hash hash = _hasher.Hash(command.Password);
    user = user.WithPasswordHash(hash);
    Res updated = await _usersRepository.Update(user);
    return updated;
  }
}