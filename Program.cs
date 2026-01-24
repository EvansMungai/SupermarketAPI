using Supermarket.API.Data;
using Supermarket.API.Extensions;
using Supermarket.API.Extensions.RouteHandlers;
using Supermarket.API.Extensions.ValidationHandlers;
using Microsoft.AspNetCore.Identity;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Extensions.ServiceHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Data Layer
builder.Services.AddDatabase(builder.Configuration);


// Extensions
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureMiddleware();

// Map Endpoints
app.MapEndpoints();

app.Run();
