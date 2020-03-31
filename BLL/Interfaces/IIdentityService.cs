using BLL.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IIdentityService
    {
        Task<ServiceActionResult> RegisterAsync(string userName, string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<ServiceActionResult> AddUserToRoleAsync(string userId, string role);
        Task<ServiceActionResult> RemoveUserFromRoleAsync(string userId, string role);
        string GetUserId(ClaimsPrincipal claimsPrincipal);
    }
}
