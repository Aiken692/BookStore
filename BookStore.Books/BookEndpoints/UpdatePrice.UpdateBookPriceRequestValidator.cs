﻿using FastEndpoints;
using FluentValidation;

namespace BookStore.Books.BookEndpoints;

public class UpdateBookPriceRequestValidator : Validator<UpdateBookPriceRequest>
{
  public UpdateBookPriceRequestValidator()
  {
    RuleFor(x => x.Id)
      .NotNull()
      .NotEqual(Guid.Empty)
      .WithMessage("A book id is required.");

    RuleFor(x => x.NewPrice)
      .GreaterThanOrEqualTo(0)
      .WithMessage("Book prices may not be negative.");
  }
}
