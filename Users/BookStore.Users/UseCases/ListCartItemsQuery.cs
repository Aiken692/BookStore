using Ardalis.Result;
using MediatR;
using BookStore.Users.CartEndpoints;

namespace BookStore.Users.UseCases;

public record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;
