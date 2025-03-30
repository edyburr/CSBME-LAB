using ClarityAPI.Lightspark;
using Microsoft.EntityFrameworkCore;
using ClarityAPI.Data;
using ClarityAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

});

builder.Services.AddSignalR();
builder.Services.AddDbContext<ClarityData>(options => options.UseSqlite("Data Source=clarity.db"));

var app = builder.Build();

app.UseCors();
app.MapPatientEndpoints();

app.MapGet("/status", () => Results.Ok("Clarity API fully operational!"));

app.MapHub<Atom>("/atom");

app.Run();