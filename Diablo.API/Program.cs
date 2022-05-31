global using FluentValidation;
global using FastEndpoints;
global using Diablo.API.Endpoints.Routes;
global using Diablo.Domain.Interfaces;
global using Diablo.Domain.Enums;
global using Diablo.Domain.Models.Entities;
global using Diablo.Domain.Models.RequestObjects;

using FastEndpoints.Swagger;
using Diablo.Data.DataAccess.ReadAccess;
using Diablo.Data.DataAccess.WriteAccess;

var builder = WebApplication.CreateBuilder();

builder.Services.AddTransient<IWritePlayerData, WritePlayerData>();
builder.Services.AddTransient<IReadPlayerData, ReadPlayerData>();

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

// https://fast-endpoints.com/wiki/Swagger-Support.html
// ^^ Where I found instructions for setting up FastEndpoints w/ Swagger
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
app.Run();
