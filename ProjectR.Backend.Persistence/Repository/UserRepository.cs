
using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Persistence.DatabaseContext;

namespace ProjectR.Backend.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {

            _context = context;
        }

        public async Task<UserModel[]> GetAllAsync()
        {
            List<User> entities = await _context.Users.ToListAsync();
            return entities.Select(e => new UserModel { Id = e.Id, PhoneNumber = e.PhoneNumber }).ToArray();
        }

        public async Task<UserModel> AddAsync(UserModel user)
        {
            User entity = new() { Id = user.Id, PhoneNumber = user.PhoneNumber, PhoneCode = user.PhoneCode, Email = user.Email, AccountType = user.AccountType, RegistrationType = user.RegistrationType };
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserModel?> GetByIdAsync(Guid id)
        {
            User? result = await _context.Users.SingleOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return null;
            }

            return new UserModel { Id = id, Email = result?.Email, PhoneNumber = result?.PhoneNumber, PhoneCode = result?.PhoneCode, AccountType = result?.AccountType, RegistrationType = result?.RegistrationType };
        }

        public async Task<UserModel> UpdateAsync(UserModel user)
        {
            User? entity = await _context.Users.SingleOrDefaultAsync(c => c.Id == user.Id);
            entity!.Email = user.Email;
            entity!.PhoneCode = user.PhoneCode;
            entity!.PhoneNumber = user.PhoneNumber;
            entity!.AccountType = user.AccountType;
            entity!.RegistrationType = user.RegistrationType;
            _context.Users.Update(entity!);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            User? entity = await _context.Users.SingleOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return null;
            }

            _context.Users.Remove(entity!);
            await _context.SaveChangesAsync();
            return entity;

        }

        public Task<bool> UserExists(string email)
        {
            return _context.Users.AnyAsync(user => user.Email == email);
        }
    }
}