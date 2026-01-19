using LMS.Application.Contracts.IRepository;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUser(long userId)
        {
            return await _dbContext.Users.Where(x => x.Id == userId && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> UpdateUser(User user)
        {
            var existingUser = await GetUser(user.Id);
            if (existingUser == null)
                return null;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUser(long userId)
        {
            var existingUser = await GetUser(userId);
            if (existingUser == null)
                return false;

            existingUser.RecordStatus = RecordStatus.Deleted;

            _dbContext.Users.Update(existingUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<User> SearchUsers(string? email, string? username, int role = (int)Role.User)
        {
            IQueryable<User> query = _dbContext.Users;

            // Filter by role
            if (role > 0)
                query = query.Where(x => (int)x.Role == role && x.RecordStatus != RecordStatus.Deleted);

            // Filter by email if provided
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(x => x.Email == email);
            }

            // Filter by username if provided
            if (!string.IsNullOrWhiteSpace(username))
            {
                query = query.Where(x => x.Username == username);
            }

            return query.AsEnumerable();

        }
    }
}
