using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Dtos.Requests;
using LMS.Application.Dtos.Responses;

namespace LMS.Application.Contracts.IService
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDto>> SignUp(SignUpRequestDto request);
        Task<ApiResponse<AuthResponseDto>> Login(AuthRequestDto request);
        Task<ApiResponse<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestDto request);
    }
}
