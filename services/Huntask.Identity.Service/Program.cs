using Huntask.Identity.Service.Infrastructure.Extensions;
using Huntask.Identity.Service.Presentation.Extensions;
using Huntask.Identity.Service.Presentation.Endpoints;

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
