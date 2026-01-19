using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Dtos.Requests;
using LMS.Application.Dtos.Responses;
using LMS.Domain.Entities;

namespace LMS.Application.Contracts.IService
{
    public interface ITokenService
    {
        Task<AuthResponseDto> AuthenticateUser(long userId);
        Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenRequestDto request);
    }
}
