using System.Reflection;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomMass();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();