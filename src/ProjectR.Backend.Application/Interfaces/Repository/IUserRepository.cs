using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<UserModel[]> GetAllAsync();
        Task<UserModel?> GetByIdAsync(Guid id);
        Task<UserModel> AddAsync(UserModel user);
        Task<UserModel> UpdateAsync(UserModel user);
        Task<User?> DeleteAsync(Guid id);
        Task<bool> UserExists(string email);
    }
}