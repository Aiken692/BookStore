using System.Reflection;
using System.Text.Json.Serialization;
using BookStore.Books;
using BookStore.Users;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
   .CreateLogger();

logger.Information("Starting Book Store...");

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((_, config) =>
  config.ReadFrom.Configuration(builder.Configuration));

//global json serializer
var jsonOptions = new JsonOptions();
jsonOptions.SerializerOptions.Converters.Add(new JsonStringEnumConverter());

builder.Services.Configure<JsonOptions>(opts =>
{
  opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.ConfigureHttpJsonOptions(opts =>
{
  opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((_, config) =>
  config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
  .AddAuthenticationJwtBearer(options =>
  {
    options.SigningKey = builder.Configuration["Auth:JwtSecret"];
    options.SigningStyle = TokenSigningStyle.Symmetric;
  })
  .AddAuthorization()
  .SwaggerDocument();

// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserServices(builder.Configuration, logger, mediatRAssemblies);

// Set up MediatR
builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthentication()
  .UseAuthorization();

app.UseFastEndpoints()
  .UseSwaggerGen();

app.Run();


