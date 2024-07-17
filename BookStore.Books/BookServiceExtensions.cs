using System.Reflection;
using BookStore.Books.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BookStore.Books;

public static class BookServiceExtensions
{
  public static IServiceCollection AddBookServices(this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<Assembly> mediatRAssemblies
    )
  {
    string? connectionString = config.GetConnectionString("BooksConnectionString");

    services.AddDbContext<BookDbContext>(options =>
    {
      options.UseNpgsql(connectionString);
    });

    //add mediator in the module
    mediatRAssemblies.Add(typeof(BookServiceExtensions).Assembly);

    //repos
    services.AddScoped<IBookRepository, BookRepository>();
    services.AddScoped<IBookService, BookService>();

    logger.Information("{Module} module services registered", "Books");

    return services;
  }
}
