using Ardalis.Result;
using MediatR;
using BookStore.Users.CartEndpoints;

namespace BookStore.Users.UseCases;

internal class ListCartItemsQueryHandler : IRequestHandler<ListCartItemsQuery,
  Result<List<CartItemDto>>>
{
  private readonly IUserRepository _userRepository;

  public ListCartItemsQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    return user.CartItems
      .Select(item => new CartItemDto(item.Id, item.BookId,
      item.Description, item.Quantity, item.UnitPrice))
      .ToList();
  }
}
