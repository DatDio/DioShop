using DioShop.Application.Contracts.Infrastructure.Identity;
using DioShop.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;
        public AccountController(IAuthService authService, IMediator mediator)
        {
            _authService = authService;
            _mediator = mediator;
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegistrationRequest request)
        {
            var responseLogin = await _authService.Register(request);
            //await _mediator.Send(new CreateCartCommand { UserId = responseLogin.UserId });

            return Ok(responseLogin);
        }

        //[HttpPost("ConfirmEmail")]
        //public async Task<ActionResult> ConfirmEmail(ConfirmAccountRequest request)
        //{
        //    var response = await _authService.ConfirmEmailAsync(request.UserId, request.Token);
        //    if (response)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        [HttpPost("Refresh")]
        public async Task<ActionResult> Refresh(RefreshRequest request)
        {
            var response = await _authService.Refresh(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("Revoke")]
        public async Task<ActionResult> Revoke()
        {
            await _authService.Revoke();
            return Ok();
        }
    }
}
