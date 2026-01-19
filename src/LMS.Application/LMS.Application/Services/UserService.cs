using LMS.Application.Contracts.IRepository;
using LMS.Application.Contracts.IService;
using LMS.Application.Dtos.Requests;
using LMS.Application.Dtos.Responses;
using LMS.Domain.Entities;
using static LMS.Application.Helpers.CustomExceptions;
using LMS.Domain.Enums;

namespace LMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserResponseDto>> AddUser(AddUserRequestDto request)
        {
            // check if user already exist(i.e email is unique)
            var existingUser =  await _userRepository.GetUserByEmail(request.Email);
            if (existingUser != null)
                throw new ValidationException("User already exists!");

            // check if user name is taken
            var usernameExists = SearchUsers("", request.Username, (int)Role.User);
            if (usernameExists.Data.Any(x => x.Username == request.Username))
                throw new ValidationException("Username is already taken!");          

            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                Role = Role.User,
                RecordStatus = RecordStatus.Active
            };

            user = await _userRepository.AddUser(user);
            var response = MapToUserResponse(user);

            return ApiResponse<UserResponseDto>.Ok(response, "User Inserted Successfully");
        }

        public async Task<ApiResponse<string>> DeleteUser(long id)
        {
            var existingUser = await _userRepository.GetUser(id) ??
               throw new NotFoundException("User Id Not Found");

            var isDeleted = await _userRepository.DeleteUser(id);
            if (!isDeleted)
                throw new Exception("An error occurred while trying to delete user");

            return ApiResponse<string>.Ok(null, "User Deleted Successfully");
        }

        public async Task<ApiResponse<UserResponseDto>> GetUser(long id)
        {
            var user = await _userRepository.GetUser(id) ??
               throw new NotFoundException("User Id Not Found");

            var userResponseDto = MapToUserResponse(user);

            return ApiResponse<UserResponseDto>.Ok(userResponseDto, "User Fetched Successfully");
        }

        public ApiResponse<IEnumerable<UserResponseDto>> SearchUsers(string? email, string? username, int role = 2)
        {
            var users = _userRepository.SearchUsers(email, username, role);
            var response = users.Select(x => MapToUserResponse(x));

            return ApiResponse<IEnumerable<UserResponseDto>>.Ok(response);
        }

        public async Task<ApiResponse<UserResponseDto>> UpdateUser(UpdateUserRequestDto request)
        {
            var user = await _userRepository.GetUser(request.Id) ??
                throw new NotFoundException("User Not Found!");

            if (request.Username != user.Username)
            {
                // check if new username already belong to a user
                var existingUsernames = _userRepository.SearchUsers(null, username: request.Username, 2).FirstOrDefault();
                if (existingUsernames != null)
                    throw new ValidationException("Username is already taken!");
            }

            user.Username = request.Username;
            user.RefreshToken = request.RefreshToken;
            user.RefreshTokenExpiryTime = request.RefreshTokenExpiryTime;

            var updatedUser = await _userRepository.UpdateUser(user);
            if (updatedUser == null)
                throw new Exception("User Update Failed, Please Try Again!");

            var userResponse = MapToUserResponse(updatedUser);

            return ApiResponse<UserResponseDto>.Ok(userResponse, "User updated successfully");

        }

        private UserResponseDto MapToUserResponse(User user)
        {
            var userResponseDto = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                RecordStatus = user.RecordStatus,
                Role = user.Role,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };

            return userResponseDto;
        }

    }
}
