#region using directives
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.OpenApi.Models;
using DevNest.VaultX.Api;
#endregion using directives

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register the custom configurations files.
builder.RegisterConfigurations();

//Register the logger injections.
builder.RegisterLogger();

//Register the mediatr service injections.
builder.RegisterMediatr();

//Register the infrastructure services.
RegisterServices.RegisterInfrastructure();

builder.Services.AddHostedService<PluginInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();