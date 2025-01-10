using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastucture.Data;

namespace ChatApp.Infrastucture.Repositories;

public class UserRepository : IUserRepository {
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<UserModel> Register(UserModel userModel) {
        await _context.User.AddAsync(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }

    public Task<UserModel> Login(UserModel userModel) {
        throw new NotImplementedException();
    }

    public Task<UserModel> GetUser(Guid id) {
        throw new NotImplementedException();
    }

    public Task<UserModel> UpdateUser(UserModel userModel) {
        throw new NotImplementedException();
    }

    public Task<UserModel> FindUser(string name) {
        throw new NotImplementedException();
    }
}