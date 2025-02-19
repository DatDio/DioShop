using DioShop.Application.Contants;
using DioShop.Application.Contracts.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Get()
        {
            var users = await _authService.ListUser();
            return Ok(users);
        }
    }
}
