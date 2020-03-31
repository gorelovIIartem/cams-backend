using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtSettings jwtSettings
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
        }

        public string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("id").Value;
        }

        public async Task<ServiceActionResult> RegisterAsync(string userName, string email, string password)
        {
            if (string.IsNullOrEmpty(userName))
                return new ServiceActionResult
                {
                    Success = false,
                    Errors = new[] { "Username is NULL or EMPTY!" }
                };

            if (string.IsNullOrEmpty(email))
                return new ServiceActionResult
                {
                    Success = false,
                    Errors = new[] { "Email is NULL or EMPTY!" }
                };

            if (string.IsNullOrEmpty(password))
                return new ServiceActionResult
                {
                    Success = false,
                    Errors = new[] { "Password is NULL or EMPTY!" }
                };

            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                return new ServiceActionResult
                {
                    Errors = new[] { $"User with email : '{email}' already exists!" }
                };

            var newUser = new User { Email = email, UserName = userName };
            var creationResult = await _userManager.CreateAsync(newUser, password);

            if (!creationResult.Succeeded)
                return new ServiceActionResult
                {
                    Success = false,
                    Errors = creationResult.Errors.Select(er => er.Description)
                };

            return new ServiceActionResult { Success = true };
        }

        public async Task<AuthenticationResult> LoginAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "UserName is NULL or EMPTY!" }
                };
            if (string.IsNullOrEmpty(password))
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "Password is NULL or EMPTY!" }
                };

            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            if (user == null)
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { $"User with username: {userName} - DOES NOT EXISTS!" }
                };

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false);
            if (!userHasValidPassword)
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "User has incorrect password!" }
                };

            return GenerateAuthenticationResultForUser(user);
        }

        public async Task<ServiceActionResult> AddUserToRoleAsync(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId))
                return new ServiceActionResult { Success = false, Errors = new[] { "Incorrect user ID!" } };
            var dbUser = await _userManager.FindByIdAsync(userId);
            if (dbUser is null)
                return new ServiceActionResult { Success = false, Errors = new[] { "User not found!" } };

            if (string.IsNullOrEmpty(role))
                return new ServiceActionResult { Success = false, Errors = new[] { "Incorrect role name!" } };
            var dbRole = await _roleManager.FindByNameAsync(role);
            if (role is null)
                return new ServiceActionResult { Success = false, Errors = new[] { $"Role with name: '{role}' not found!" } };

            var result = await _userManager.AddToRoleAsync(dbUser, dbRole.Name);
            if (!result.Succeeded)
                return new ServiceActionResult { Success = result.Succeeded, Errors = result.Errors.Select(er => er.Description) };
            return new ServiceActionResult { Success = true };
        }

        public async Task<ServiceActionResult> RemoveUserFromRoleAsync(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId))
                return new ServiceActionResult { Success = false, Errors = new[] { "Incorrect user ID!" } };
            var dbUser = await _userManager.FindByIdAsync(userId);
            if (dbUser is null)
                return new ServiceActionResult { Success = false, Errors = new[] { "User not found!" } };

            if (string.IsNullOrEmpty(role))
                return new ServiceActionResult { Success = false, Errors = new[] { "Incorrect role name!" } };
            var dbRole = await _roleManager.FindByNameAsync(role);
            if (role is null)
                return new ServiceActionResult { Success = false, Errors = new[] { $"Role with name: '{role}' not found!" } };

            var userRoles = await _userManager.GetRolesAsync(dbUser);
            if (!userRoles.Contains(role))
                return new ServiceActionResult { Success = false, Errors = new[] { $"User don`t have role: '{role}'!" } };
            var result = await _userManager.RemoveFromRoleAsync(dbUser, role);
            if (!result.Succeeded)
                return new ServiceActionResult { Success = result.Succeeded, Errors = result.Errors.Select(er => er.Description) };
            return new ServiceActionResult { Success = true };
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(User newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
