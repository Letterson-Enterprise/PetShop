using Microsoft.AspNetCore.Mvc;
using backend_petshop.DTOs;
using backend_petshop.Interfaces;

namespace backend_petshop.Controllers
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

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.Login(loginDto);
            return Ok(response);
        }
    }
}
