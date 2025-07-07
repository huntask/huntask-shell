using Huntask.Identity.Service.Infrastructure.Extensions;
using Huntask.Identity.Service.Presentation.Extensions;
using Huntask.Identity.Service.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterAspNetServices(builder);

var app = builder
  .ConfigureBuilder()
  .Build();
app.ConfigureWebApplication();
app.RegisterIdentityEndpoints();

app.Run();
