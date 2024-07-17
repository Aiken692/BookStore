namespace BookStore.Books.BookEndpoints;

public class ListBooksResponse
{
  public List<BookDto> Books { get; set; } = new List<BookDto>();
}

