namespace BookStore.Books;

public record BookDto(
  Guid Id, string Title, 
  string Author, 
  decimal Price);
