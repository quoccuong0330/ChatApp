using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces;

public interface IUserRepository {
    public Task<UserModel> Register(UserModel userModel);
    public Task<UserModel> Login(UserModel userModel);
    public Task<UserModel> GetUser(Guid id);
    public Task<UserModel> UpdateUser(UserModel userModel);
    public Task<UserModel> FindUser(string name);
}