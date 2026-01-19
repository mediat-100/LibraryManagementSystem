using System;
using LMS.Application.Dtos.Requests;
using LMS.Application.Dtos.Responses;
using LMS.Domain.Enums;

namespace LMS.Application.Contracts.IService
{
    public interface IUserService
    {
        ApiResponse<IEnumerable<UserResponseDto>> SearchUsers(string? email, string? username, int role = (int)RecordStatus.Active);
        Task<ApiResponse<UserResponseDto>> AddUser(AddUserRequestDto user);
        Task<ApiResponse<UserResponseDto>> GetUser(long id);
        Task<ApiResponse<string>> DeleteUser(long id);
        Task<ApiResponse<UserResponseDto>> UpdateUser(UpdateUserRequestDto request);
    }
}
