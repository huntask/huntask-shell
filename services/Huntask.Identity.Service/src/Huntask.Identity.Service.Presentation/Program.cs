using Huntask.Identity.Service.Infrastructure.Extensions;
using Huntask.Identity.Service.Presentation.Extensions;
using Huntask.Common.Infrastructure.Extensions;

[assembly: ExcludeFromCodeCoverage]

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddSettings()
  .AddInfrastructureServices()
  .AddPresentationServices()
  .AddCommonDecorations()
  .AddHealthCheckServices();

var app = builder
  .ConfigureBuilder()
  .Build();

app
  .ConfigurePresentation()
  .ConfigureCommonDecorations()
  .ConfigureHealthChecks();

app.Run();
