using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Huntask.Identity.Service.Presentation.Models;
using Huntask.Identity.Service.Domain.Entities;
using Huntask.Identity.Service.Domain.Repositories;
using Huntask.Identity.Service.Infrastructure.Extensions;

namespace Huntask.Identity.Service.Presentation.Endpoints;

public static class TasksCrudEndpoints
{
  private static readonly string[] tags = ["Tasks"];
  private const string NameIsRequiredError = "Name is required";
  private const string Prefix = "api/tasks";

  public static void RegisterTaskCrudEndpoints(this WebApplication app)
  {
    app.MapGet($"{Prefix}/{{id}}", GetAsync)
      .WithTags(tags)
      .CacheOutput(p =>
        p.Expire(TimeSpan.FromSeconds(ApiUtilConstants.CacheTTL.Warm))
         .SetVaryByQuery("id")
         .SetVaryByHeader(ApiUtilConstants.Headers.Authorization)
         .Tag("Task"))
      .WithMetadata(new ResponseCacheAttribute
      {
        VaryByQueryKeys = ["id"],
        VaryByHeader = ApiUtilConstants.Headers.Authorization,
        Duration = ApiUtilConstants.CacheTTL.Warm
      });
    app.MapGet($"{Prefix}/all", GetAllAsync)
      .WithTags(tags)
      .CacheOutput(p =>
        p.Expire(TimeSpan.FromSeconds(ApiUtilConstants.CacheTTL.Warm))
         .SetVaryByHeader(ApiUtilConstants.Headers.Authorization)
         .Tag("Tasks_All"))
      .WithMetadata(new ResponseCacheAttribute
      {
        VaryByHeader = ApiUtilConstants.Headers.Authorization,
        Duration = ApiUtilConstants.CacheTTL.Warm
      });

    app.MapPost($"{Prefix}", CreateAsync).WithTags(tags);
    app.MapPost($"{Prefix}/{{id}}", UpdateAsync).WithTags(tags);
    app.MapDelete($"{Prefix}/{{id}}", DeleteAsync).WithTags(tags);
  }

  [Authorize]
  private static async Task<IResult> GetAsync(
    int id,
    ITaskItemsRepository taskRepository,
    ClaimsPrincipal principal)
  {
    var user = principal.ToUserEntity();

    var task = await taskRepository.GetAsync(id, user.Id);

    return task is { IsNull: true }
      ? Results.NotFound()
      : Results.Ok(new ApiResponse<TaskItem>(true, task));
  }

  [Authorize]
  private static async Task<IResult> GetAllAsync(ITaskItemsRepository taskRepository, ClaimsPrincipal principal)
  {
    var user = principal.ToUserEntity();
    Log.Logger.Debug("Getting Tasks for the user: {UserName}.", user.UserName);

    var Tasks = await taskRepository.GetAllAsync(user.Id);
    return Results.Ok(new ApiResponse<IEnumerable<TaskItem>>(true, Tasks));
  }

  [Authorize]
  private static async Task<IResult> CreateAsync(
    TaskItem task,
    ITaskItemsRepository taskRepository,
    ClaimsPrincipal principal)
  {
    if (string.IsNullOrWhiteSpace(task.Name))
    {
      Log.Logger.Debug("Task creation has failed. Name should not be null or empty.\n{Task}", JsonSerializer.Serialize(task));
      return Results.BadRequest(NameIsRequiredError);
    }

    var user = principal.ToUserEntity();
    var newTask = await taskRepository.CreateAsync(task, user.Id);

    Log.Logger.Debug("Task has been created for user: {UserName}.\n{Task}", user.UserName, JsonSerializer.Serialize(task));
    return Results.Created($"api/tasks/{newTask.Id}", new ApiResponse<TaskItem>(true, newTask));
  }

  [Authorize]
  private static async Task<IResult> UpdateAsync(
    int id,
    TaskItem task,
    ITaskItemsRepository TaskRepository,
    ClaimsPrincipal principal)
  {
    if (string.IsNullOrWhiteSpace(task.Name))
    {
      Log.Logger.Debug("Task update has failed. Name should not be null or empty.\n{Task}", JsonSerializer.Serialize(task));
      return Results.BadRequest(new ApiResponse<object>(false, task, NameIsRequiredError));
    }

    var user = principal.ToUserEntity();
    var updated = await TaskRepository.UpdateAsync(id, task, user.Id);

    if (updated is { IsNull: true })
    {
      Log.Logger.Debug("Task {id} update has failed.\n{Task}", id, JsonSerializer.Serialize(task));
      return Results.NotFound();
    }

    Log.Logger.Debug("Task {id} is getting updated.\n{Task}", id, JsonSerializer.Serialize(task));
    return Results.Ok(new ApiResponse<TaskItem>(true, updated));
  }

  [Authorize]
  private static async Task<IResult> DeleteAsync(
    int id,
    ITaskItemsRepository taskRepository,
    ClaimsPrincipal principal)
  {
    var user = principal.ToUserEntity();
    var deleted = await taskRepository.DeleteAsync(id, user.Id);

    if (deleted is { IsNull: true })
    {
      Log.Logger.Debug("Task {id} deletion has failed.\n{Task}", id, JsonSerializer.Serialize(deleted));
      return Results.NotFound();
    }

    Log.Logger.Debug("Task {id} is getting deleted.\n{Task}", id, JsonSerializer.Serialize(deleted));
    return Results.Ok(new ApiResponse<TaskItem>(true, deleted));
  }
}