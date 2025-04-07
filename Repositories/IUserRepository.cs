using System.Threading.Tasks;
using ScientiaMobilis.Models;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
}