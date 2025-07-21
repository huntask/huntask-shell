using Huntask.Assets.Service.Infrastructure.Extensions;
using Huntask.Assets.Service.Presentation.Extensions;
using Huntask.Assets.Service.Presentation.Endpoints;
using Huntask.Common.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
  .AddInfrastructureServices(builder.Configuration)
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
