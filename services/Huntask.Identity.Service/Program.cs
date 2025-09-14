using Huntask.Identity.Service.Infrastructure.Extensions;
using Huntask.Identity.Service.Presentation.Extensions;
using Huntask.Identity.Service.Presentation.Endpoints;
using Huntask.Common.Presentation.Extensions;
using Huntask.Common.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddSettings()
  .AddInfrastructureServices()
  .AddPresentationServices()
  .AddCommonDecorations();

var app = builder
  .ConfigureBuilder()
  .Build();

app
  .ConfigurePresentation()
  .ConfigureCommonDecorations()
  .RegisterHealthcheckEndpoint();

app.Run();
