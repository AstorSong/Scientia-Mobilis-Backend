using System.Threading.Tasks;
using ScientiaMobilis.Models;
using ScientiaMobilis.Repositories;

namespace ScientiaMobilis.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Registration (Sign Up)
        public async Task<User> RegisterAsync(User user)
        {
            // 1. Check if user with email already exists
            var existing = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existing != null)
            {
                throw new System.Exception("Email is already in use.");
            }

            // 2. Create new user in Firebase
            User created = await _userRepository.CreateUserAsync(user);
            return created;
        }

        // Sign-in (Example approach - in real usage, rely on Firebase Auth tokens)
        public async Task<User> SignInAsync(string email, string password)
        {
            // We'll rely on Firebase to do the actual sign-in logic, 
            // but for demonstration, let's assume we simply retrieve user by email:

            var existing = await _userRepository.GetUserByEmailAsync(email);
            if (existing == null)
            {
                throw new System.Exception("User not found.");
            }

            // We can't directly verify the password if using Firebase as the sole store.
            // Typically, you'd sign in using the Firebase Auth client in the front-end,
            // then confirm via token validation. For demonstration:
            if (password != existing.Password)
            {
                throw new System.Exception("Invalid credentials.");
            }

            return existing;
        }
    }
}