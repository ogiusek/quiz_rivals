namespace Common.Types;

public record EventListener(
  Id Id,
  Func<Task> Action
);

public record EventListener<T>(
  Id Id,
  Func<T, Task> Action
);
