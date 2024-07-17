using Ardalis.Result;
using BookStore.Books.Contracts;
using MediatR;

namespace BookStore.Books.UseCases;
internal class BookDetailsQueryHandler :
  IRequestHandler<BookDetailsQueryRequest, Result<BookDetailsResponse>>
{
  private readonly IBookService _bookService;

  public BookDetailsQueryHandler(
    IBookService bookService)
  {
    _bookService = bookService;
  }

  public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQueryRequest request, CancellationToken cancellationToken)
  {
    var book = await _bookService.GetBookByIdAsync(request.BookId);

    if (book is null)
    {
      return Result.NotFound();
    }

    //Mapping the book to the response
    var response = new BookDetailsResponse(
      book.Id, 
      book.Title, 
      book.Author,
      book.Price);

    return response;
  }
}
