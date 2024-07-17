using Microsoft.EntityFrameworkCore;

namespace BookStore.Users.Persistence;
internal class UserRepository : IUserRepository
{
  private readonly UsersDbContext _dbContext;

  public UserRepository(UsersDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
  {
    return _dbContext.ApplicationUsers
      .Include(user => user.CartItems)
      .SingleAsync(user => user.Email == email);
  }

  public Task SaveChangesAsync()
  {
    return _dbContext.SaveChangesAsync();
  }
}
