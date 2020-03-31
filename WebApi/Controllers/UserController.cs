using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApi.Filters;
using WebApi.Models.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<UserController> _logger;

        public UserController(IIdentityService identityService, ILogger<UserController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        [HttpPost(WebApiRoutes.User.Login)]
        [ValidateModelState]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var loginResult = await _identityService.LoginAsync(loginModel.UserName, loginModel.Password);
            return loginResult.Success
                    ? (IActionResult)Ok(loginResult.Token)
                    : BadRequest(loginResult.Errors);
        }

        [HttpPost(WebApiRoutes.User.Register)]
        [ValidateModelState]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var registerResult = await _identityService.RegisterAsync(registerModel.UserName, registerModel.Email, registerModel.Password);
            return registerResult.Success
                    ? (IActionResult)Ok()
                    : BadRequest(registerResult.Errors);
        }
    }
}