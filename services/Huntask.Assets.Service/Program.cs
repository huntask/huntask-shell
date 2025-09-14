using Huntask.Assets.Service.Infrastructure.Extensions;
using Huntask.Assets.Service.Presentation.Extensions;
using Huntask.Assets.Service.Presentation.Endpoints;
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
