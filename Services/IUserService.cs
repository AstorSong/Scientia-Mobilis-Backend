using System.Threading.Tasks;
using ScientiaMobilis.Models;

namespace ScientiaMobilis.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user);
        Task<User> SignInAsync(string email, string password);
    }
}