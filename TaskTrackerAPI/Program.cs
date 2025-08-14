using Microsoft.AspNetCore.Diagnostics;
using TaskTrackerAPI.Extensions;
using TaskTrackerAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Register Service Extensions
builder.Services.RegisterAppConfigurations(builder.Configuration);
builder.Services.RegisterPersistenceService(builder.Configuration);
builder.Services.RegisterInfrastructureServices();
builder.Services
    .AddJwtAuthentication(builder.Configuration)
    .AddRoleBasedAuthorization();
builder.Services.RegisterSwaggerServices();

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>(); //Register Ex

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();