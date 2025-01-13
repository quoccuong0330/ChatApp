using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Application.Services;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastucture.Data;
using ChatApp.Infrastucture.SignalR.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastucture.Repositories;

public class UserRepository : IUserRepository {
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context, JwtService jwtService) {
        _context = context;
    }
    
    public async Task<UserModel?> Register(UserModel? userModel) {
        await _context.User.AddAsync(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }

    public async Task<UserModel?> Login(string email, string password) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(email));
        if (user is null) return null;
        return PasswordHasher.IsCorrectPassword(password, user.Password) ? user : null;
    }

    public async Task<UserModel?> GetUser(Guid id) {
        return await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<UserModel> UpdateUser(Guid id,UserModel userModel) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (user is null) return null;
        user.FirstName = userModel.FirstName;
        user.LastName = userModel.LastName;
        user.DateOfBirth = userModel.DateOfBirth;
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<UserModel> FindUser(string name) {
        throw new NotImplementedException();
    }

    public async Task<UserModel?> UpdateAvatar(Guid id, string avatarUrl) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (user is null) return null;
        user.Avatar = avatarUrl;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdatePassword(Guid id, string oldPassword, string newPassword) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (user is null) return false;
        if (!PasswordHasher.IsCorrectPassword(oldPassword, user.Password)) return false;
        user.Password = PasswordHasher.HashPasswordUser(newPassword);
        await _context.SaveChangesAsync();
        return true;
    }
}