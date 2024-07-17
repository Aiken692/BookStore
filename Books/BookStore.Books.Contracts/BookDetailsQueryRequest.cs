using Ardalis.Result;
using MediatR;

namespace BookStore.Books.Contracts;

public record BookDetailsQueryRequest(Guid BookId) : IRequest<Result<BookDetailsResponse>>;
