using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class AuthRepository : IAuthRepository
    {
        private readonly PlazzoContext _context;

        public AuthRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }