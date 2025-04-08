using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using ScientiaMobilis.Models;

namespace ScientiaMobilis.Repositories
{
    public class FirebaseUserRepository : IUserRepository
    {
        public async Task<User> CreateUserAsync(User user)
        {
            // Create Firebase user
            var args = new UserRecordArgs
            {
                Email = user.Email,
                Password = user.Password,
                DisplayName = user.DisplayName,
                EmailVerified = user.EmailVerified
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

            // Return domain user with UID from Firebase
            return new User
            {
                Uid = userRecord.Uid,
                Email = userRecord.Email,
                DisplayName = userRecord.DisplayName,
                EmailVerified = userRecord.EmailVerified,

                // For demo purposes
                Password = user.Password
            };
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            // Attempt to fetch user record by email
            try
            {
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);

                if (userRecord == null) return null;

                return new User
                {
                    Uid = userRecord.Uid,
                    Email = userRecord.Email,
                    DisplayName = userRecord.DisplayName,
                    EmailVerified = userRecord.EmailVerified
                };
            }
            catch (FirebaseAuthException)
            {
                // Email not found or other error
                return null;
            }
        }
    }
}