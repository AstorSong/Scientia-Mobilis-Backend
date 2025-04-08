using System.Threading.Tasks;
using ScientiaMobilis.Models;

namespace ScientiaMobilis.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
    }
}