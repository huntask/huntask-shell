using Huntask.Identity.Service.Domain.Entities;

namespace Huntask.Identity.Service.Domain.Repositories;

public interface ITaskItemsRepository
{
  public Task<TaskItem> GetAsync(int id, string userId);
  public Task<IEnumerable<TaskItem>> GetAllAsync(string userId);
  public Task<TaskItem> CreateAsync(TaskItem todo, string userId);
  public Task<TaskItem> UpdateAsync(int id, TaskItem newTodo, string userId);
  public Task<TaskItem> DeleteAsync(int id, string userId);
}