var builder = WebApplication.CreateBuilder(args);
// services and containers registrations


var app = builder.Build();
// ----------------------------------------------------------
// http request pipeline configuration

app.Run();
