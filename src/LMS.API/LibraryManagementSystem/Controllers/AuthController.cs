using LMS.Application.Contracts.IService;
using LMS.Application.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(SignUpRequestDto request)
        {
            var response = await _authService.SignUp(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(AuthRequestDto request)
        {
            var response = await _authService.Login(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshToken(request);
            return Ok(response);
        }
    }
}
