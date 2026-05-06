using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
// services and containers registrations

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();
// ----------------------------------------------------------
// http request pipeline configuration
app.UseApiServices();

// esecuzione di automigrazione del db, senza dover eseguire manualmente il comando Add-Migration e Update-Database
if(app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();
