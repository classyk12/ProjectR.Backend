using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IUserManager
    {
        Task<UserModel[]> GetAllAsync();
        Task<ResponseModel<UserModel>> GetByIdAsync(Guid id);
        Task<ResponseModel<UserModel>> AddAsync(AddUserModel addUserModel);
        Task<ResponseModel<UserModel>> UpdateAsync(UserModel user);
        Task<BaseResponseModel?> DeleteAsync(Guid Id);
    }
}