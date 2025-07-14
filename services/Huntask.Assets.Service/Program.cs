using Huntask.Assets.Service.Infrastructure.Extensions;
using Huntask.Assets.Service.Presentation.Extensions;
using Huntask.Assets.Service.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services
  .AddInfrastructureServices(builder.Configuration)
  .AddPresentationServices()
  .AddDecorations();

var app = builder
  .ConfigureBuilder()
  .Build();

app
  .ConfigurePresentation()
  .ConfigureDecorations()
  .RegisterHealthcheckEndpoint();

app.Run();
