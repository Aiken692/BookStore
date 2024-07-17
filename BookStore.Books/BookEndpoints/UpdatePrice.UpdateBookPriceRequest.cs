namespace BookStore.Books.BookEndpoints;

public record UpdateBookPriceRequest(Guid Id, decimal NewPrice);
