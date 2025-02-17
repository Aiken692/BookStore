﻿using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Users.UserEndpoints;

internal class Login : Endpoint<UserLoginRequest>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public Login(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }
  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UserLoginRequest request,
    CancellationToken ct)
  {
    var user = await _userManager.FindByEmailAsync(request.Email!);
    if (user == null)
    {
      await SendUnauthorizedAsync();
      return;
    }
    var loginSuccessful = await _userManager.CheckPasswordAsync(user, request.Password);

    if (!loginSuccessful)
    {
      await SendUnauthorizedAsync();
      return;
    }

    var jwtSecret = Config["Auth:JwtSecret"]!;


    var token = JwtBearer.CreateToken(options =>
    {
      options.SigningKey = jwtSecret;
      options.User.Claims.Add(new Claim("EmailAddress", user.Email!));
    });

    await SendAsync(token);
  }

}
