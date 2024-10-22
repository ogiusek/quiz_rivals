using Common.Adapters;
using Common.App.Abstractions;
using Common.Types;
using FileSaver.Adapter;
using FileSaver.Adapter.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Users.App.Exceptions;
using Users.Core.Models;

namespace Users.App.Commands.SetPhoto;

internal sealed class SetPhotoHandler(
  IFileStorage fileStorage,
  IRepository<User> usersRepository
) : ICustomCommandHandler<SetPhotoCommand>
{
  bool _runAfterExecute = false;
  bool ICustomCommandHandler<SetPhotoCommand>.RunAfterExecute { get => _runAfterExecute; set => _runAfterExecute = value; }
  FilePath currentPhotoPath;

  async Task<Res> ICustomCommandHandler<SetPhotoCommand>.Execute(SetPhotoCommand command)
  {
    User user = await usersRepository.Get.AsAsyncEnumerable().Where(u => u.Id.Value == command.Id).SingleOrDefaultAsync();
    if (user is null)
    {
      return Res.Fail(new UserNotFound());
    }
    currentPhotoPath = user.PhotoPath;

    Res<FilePath> photoPath = await fileStorage.Save(command.Photo);
    if (photoPath.IsFailure)
    {
      return photoPath;
    }

    user = user.WithPhotoPath(photoPath.Value);
    Res updated = await usersRepository.Update(user);
    if (updated.IsFailure)
    {
      await fileStorage.Remove(photoPath.Value);
      return updated;
    }
    _runAfterExecute = true;
    return updated;
  }
  async Task ICustomCommandHandler<SetPhotoCommand>.AfterExecute()
  {
    if (currentPhotoPath.Path == User.DefaultPhotoPath.Path)
    {
      return;
    }
    await fileStorage.Remove(currentPhotoPath);
  }
}