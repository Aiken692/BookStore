namespace BookStore.Users.UserEndpoints;

public record UserLoginRequest(
  string Email, 
  string Password);
