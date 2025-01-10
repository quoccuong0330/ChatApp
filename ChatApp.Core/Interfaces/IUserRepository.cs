using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces;

public interface IUserRepository {
    public Task<UserModel?> Register(UserModel? userModel);
    public Task<UserModel> Login(string email, string password);
    public Task<UserModel?> GetUser(Guid id);
    public Task<UserModel?> UpdateUser(Guid id,UserModel userModel);
    public Task<UserModel> FindUser(string name);
    Task<UserModel?> UpdateAvatar(Guid id, string avatarUrl);
    Task<bool> UpdatePassword(Guid id, string oldPassword, string newPassword);
}