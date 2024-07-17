using Ardalis.Result;
using BookStore.Books.Contracts;
using MediatR;

namespace BookStore.Users.UseCases;

public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Result>
{
  private readonly IUserRepository _userRepository;
  private readonly IMediator _mediator;

  public AddItemToCartHandler(IUserRepository userRepository,
    IMediator mediator)
  {
    _userRepository = userRepository;
    _mediator = mediator;
  }

  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    var query = new BookDetailsQueryRequest(request.BookId);

    var result = await _mediator.Send(query);

    if (result.Status == ResultStatus.NotFound) return Result.NotFound();

    var bookDetails = result.Value;

    var description = $"{bookDetails.Title} by {bookDetails.Author}";

    var newCartItem = new CartItem(request.BookId,
      description,
      request.Quantity,
      bookDetails.Price);

    user.AddItemToCart(newCartItem);

    await _userRepository.SaveChangesAsync();
    return Result.Success();
  }
}
