using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel[]> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<ResponseModel<UserModel>> GetByIdAsync(Guid id)
        {
            UserModel? result = await _userRepository.GetByIdAsync(id);
            return new ResponseModel<UserModel>(message: result != null ?
            "User retrieved successfully." :
            "User not found.", result, result != null);
        }

        public async Task<ResponseModel<UserModel>> AddAsync(AddUserModel userModel)
        {
            bool userExists = await _userRepository.UserExists(userModel.Email);
            if (userExists)
            {
                return new ResponseModel<UserModel>(message: "User already exists.", status: false, data: null);
            }

            UserModel model = new UserModel
            {
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                RegistrationType = userModel.RegistrationType,
                AccountType = userModel.AccountType,
                PhoneCode = userModel.PhoneCode
            };
            UserModel result = await _userRepository.AddAsync(model);
            return new ResponseModel<UserModel>(message: "User added successfully.", data: result, status: true);
        }

        public async Task<ResponseModel<UserModel>> UpdateAsync(UserModel user)
        {
            UserModel? existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return new ResponseModel<UserModel>(message: "User not found.", data: default, status: false);
            }

            UserModel result = await _userRepository.UpdateAsync(user);
            return new ResponseModel<UserModel>(message: "User updated successfully.", data: result, status: true);
        }

        public async Task<BaseResponseModel?> DeleteAsync(Guid id)
        {
            UserModel? existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return new BaseResponseModel(message: "User not found.", status: false);
            }

            await _userRepository.DeleteAsync(existingUser.Id);
            return new BaseResponseModel(message: "User deleted successfully.", status: true);
        }
    }
}