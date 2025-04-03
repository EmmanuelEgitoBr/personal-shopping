using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Auth.Api.Models.Dtos;
using Personal.Shopping.Services.Auth.Api.Services.Interfaces;

namespace Personal.Shopping.Services.Auth.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ResponseDto> RegisterUser([FromBody] RegistrationRequestDto requestDto)
        {
            var result = await _authService.RegisterAsync(requestDto);

            return result;
        }

        [HttpPost("login")]
        public async Task<ResponseDto> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authService.LoginAsync(loginRequestDto);

            return result;
        }

        [HttpPost("assign-role/{email}/{role}")]
        public async Task<ResponseDto> AssignRole(string email, string role)
        {
            var result = await _authService.AssignRoleToUser(email, role);

            return result;
        }
    }
}
