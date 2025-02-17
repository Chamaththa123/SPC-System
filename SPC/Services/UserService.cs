using SPC.DataAccess;
using SPC.Helpers;
using SPC.Models;
using System.IdentityModel.Tokens.Jwt;

namespace SPC.Services
{
    public class UserService
    {
        private readonly UserDAL _userDAL;

        public UserService(UserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public async Task<bool> RegisterUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash password
            int result = await _userDAL.RegisterUser(user);
            return result > 0;
        }


        public async Task<string> Login(LoginRequest request)
        {
            var user = await _userDAL.GetUserByEmail(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null; // Invalid credentials

            if (user.Status == 0)
                return "User not active";

            return JwtHelper.GenerateToken(user); // Generate JWT
        }


        public async Task<User> GetUserDetailsByEmail(string email)
        {
            var user = await _userDAL.GetUserByEmail(email);
            if (user == null)
                return null;

            return new User
            {
                IdUser = user.IdUser,
                Name = user.Name,
                Email = user.Email,
                Contact = user.Contact,
                Role = user.Role,
                Status = user.Status,
                BranchId = user.BranchId,
            };
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userDAL.GetAllUsers();
        }

        public async Task<bool> ActivateUser(int userId)
        {
            var user = await _userDAL.GetUserById(userId);

            if (user == null)
                return false;

            if (user.Status == 1)
                return false;

            return await _userDAL.UpdateUserStatus(userId, 1);
        }

    }
}
