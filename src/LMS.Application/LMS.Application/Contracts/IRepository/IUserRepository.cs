using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Contracts.IRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUser(long userId);
        Task<User?> GetUserByEmail(string email);
        Task<User> AddUser(User user);
        Task<User?> UpdateUser(User user);
        Task<bool> DeleteUser(long userId);
        IEnumerable<User> SearchUsers(string? email, string? username, int role = (int)Role.User);
    }
}
