using Microsoft.EntityFrameworkCore;
using Huntask.Identity.Service.Domain.Entities;
using Huntask.Identity.Service.Domain.Repositories;
using Huntask.Identity.Service.Infrastructure.Contexts;

namespace Huntask.Identity.Service.Infrastructure.Repositories;

public class TaskItemsRepository : ITaskItemsRepository
{
  private readonly TasksContext db;

  public TaskItemsRepository(TasksContext db)
  {
    this.db = db;
  }

  public async Task<TaskItem> GetAsync(int id, string userId)
  {
    if (await db.Todos.FindAsync(id) is TaskItem todo)
    {
      return todo.UserId == userId ? todo : TaskItem.NullTodo;
    }

    return TaskItem.NullTodo;
  }

  public async Task<IEnumerable<TaskItem>> GetAllAsync(string userId)
  {
    return await db.Todos.Where(t => t.UserId == userId).ToListAsync();
  }

  public async Task<TaskItem> CreateAsync(TaskItem todo, string userId)
  {
    todo.UserId = userId;

    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return todo;
  }

  public async Task<TaskItem> UpdateAsync(int id, TaskItem newTodo, string userId)
  {
    var todo = await db.Todos.FindAsync(id);
    if (todo is null || todo.UserId != userId)
    {
      return TaskItem.NullTodo;
    }

    todo.Name = newTodo.Name;
    todo.IsComplete = newTodo.IsComplete;

    await db.SaveChangesAsync();
    return todo;
  }

  public async Task<TaskItem> DeleteAsync(int id, string userId)
  {
    if (await db.Todos.FindAsync(id) is TaskItem todo && todo.UserId == userId)
    {
      db.Todos.Remove(todo);
      await db.SaveChangesAsync();
      return todo;
    }

    return TaskItem.NullTodo;
  }
}