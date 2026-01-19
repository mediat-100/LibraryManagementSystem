using LMS.Application.Contracts.IRepository;
using LMS.Application.Contracts.IService;
using LMS.Application.Dtos.Requests;
using LMS.Application.Dtos.Responses;
using LMS.Application.Helpers;
using LMS.Domain.Enums;
using Microsoft.Extensions.Logging;
using static LMS.Application.Helpers.CustomExceptions;

namespace LMS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserService userService, ITokenService tokenService, IUserRepository userRepository)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
       

        
        public async Task<ApiResponse<AuthResponseDto>> SignUp(SignUpRequestDto request)
        {
            if (!Utils.IsValidEmail(request.Email))
                throw new ValidationException("A valid email is required.");

            if (request.Username.Length < 8)
                throw new ValidationException("Username must be at least 8 characters long.");

            if (request.Password.Length < 8)
                throw new ValidationException("Password must be at least 8 characters long.");

            if (!request.Password.Any(char.IsUpper))
                throw new ValidationException("Password must contain at least one uppercase letter.");

            if (!request.Password.Any(char.IsLower))
                throw new ValidationException("Password must contain at least one lowercase letter.");

            if (!request.Password.Any(char.IsDigit))
                throw new ValidationException("Password must contain at least one number.");

            if (!request.Password.Any(ch => "!@#$%^&*()_+-=[]{}|;:'\",.<>?/".Contains(ch)))
                throw new ValidationException("Password must contain at least one special character.");

            if (request.Password.Contains(" "))
                throw new ValidationException("Password cannot contain spaces.");

            // check if email already exist
            ApiResponse<IEnumerable<UserResponseDto>> existingUser = _userService.SearchUsers(request.Email, null, (int)Role.User);
            if (existingUser.Data.Count() > 0)
                throw new ValidationException("User already exist!");

            var userRequest = new AddUserRequestDto
            {
                Email = request.Email,
                Username = request.Username,
                Password = Utils.HashPassword(request.Password)
            };

            var userResp = await _userService.AddUser(userRequest);

            if (!userResp.Success && userResp.Data != null)
                throw new ValidationException("User Signup Failed!");

            var authResponse = await _tokenService.AuthenticateUser(userResp.Data.Id);

            return ApiResponse<AuthResponseDto>.Ok(authResponse, "Signup Successful");
        }

        public async Task<ApiResponse<AuthResponseDto>> Login(AuthRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new ValidationException("Please input your email and password");

            // search for existing user by email
            var user = await _userRepository.GetUserByEmail(request.Email) ??
                throw new UnauthorizedException("Incorrect Email or Password");

            var verifyPassword = Utils.VerifyPassword(request.Password, user.Password);
            if (!verifyPassword)
                throw new UnauthorizedException("Incorrect Email or Password!");

            var authResponse = await _tokenService.AuthenticateUser(user.Id);

            return ApiResponse<AuthResponseDto>.Ok(authResponse, "Login successful");
        }

        public async Task<ApiResponse<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var response = await _tokenService.RefreshToken(request);
            return ApiResponse<RefreshTokenResponseDto>.Ok(response);
        }
    }
}
