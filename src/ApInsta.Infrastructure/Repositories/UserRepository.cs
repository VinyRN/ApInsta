using ApInsta.Domain.Entities;
using ApInsta.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApInsta.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Email == login);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
