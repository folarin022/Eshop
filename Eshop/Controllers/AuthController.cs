using Eshop.Dto.AuthModel;
using Eshop.Service;
using Eshop.Service.Inteterface;
using EssenceShop.Models;
using EssenceShop.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EssenceShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request,CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterUser(request,cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request,CancellationToken cancellationToken)
        {
            var result = await _authService.LoginClients(request, cancellationToken);
            if (!result.IsSuccess) return Unauthorized(result);
            return Ok(result);
        }
    }

}
