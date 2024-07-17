using System.Reflection;
using BookStore.Books;
using FastEndpoints;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
   .CreateLogger();

logger.Information("Starting Book Store...");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();


// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Configuration, logger, mediatRAssemblies);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseFastEndpoints();

app.Run();

public partial class Program { } // needed for tests

