using MediatR;
using Microsoft.AspNetCore.Mvc;
using Huntask.Identity.Service.Application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.RegisterUserCommand;
using Huntask.Identity.Service.Application.Models;
using Huntask.Identity.Service.Presentation.Models;

namespace Huntask.Identity.Service.Presentation.Endpoints;

public static class HealthcheckEndpoint
{
  public static void RegisterHealthcheckEndpoint(this WebApplication app)
  {
    app
      .MapGet("api/healthcheck", () => Results.Ok("Identity Service is running"))
      .WithName("Healthcheck")
      .WithTags("Healthcheck");
  }
}
