using ChatApp.Application.Dtos.Requests.User;
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
    private readonly JwtService _jwtService;
    public UserRepository(ApplicationDbContext context, JwtService jwtService) {
        _context = context;
        _jwtService = jwtService;
    }
    
    public async Task<RecordUserResponseDto> Register(UserModel userModel) {
        var isEmailExist = await _context.User.AnyAsync(x => x.Email.Equals(userModel.Email));
        if(isEmailExist is true) 
            return new RecordUserResponseDto(false,new UserModel{}, "Email has already exist.");
        
        var userCreate = await _context.User.AddAsync(userModel);
        await _context.SaveChangesAsync();
        
        var response = new RecordUserResponseDto(true, userModel, "Registration successful");
        return response;
    }

    public async Task<LoginResponseDto> Login(LoginDto loginDto) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(loginDto.Email));
        if (user is null) return new LoginResponseDto(false, "Email does not exist");
        if (!PasswordHasher.IsCorrectPassword(loginDto.Password, user.Password))
            return new LoginResponseDto(false, "Password is incorrect");
        
        var refreshTokenObject = _jwtService.CreateRefreshToken();
        var refreshModel = new RefreshTokenModel {
            Token = refreshTokenObject.Token,
            Expires = refreshTokenObject.Expired,
            IsRevoked = false,
            UserId = user.Id
        };
        await _context.RefreshTokens.AddAsync(refreshModel);
        await _context.SaveChangesAsync();
        
        return new LoginResponseDto(true, "Login successfully",
            _jwtService.CreateAssessToken(user),
            refreshTokenObject.Token);
    }

    public async Task<RecordUserResponseDto> LogOut(Guid id,string refreshTokenFromRequest) {
        var refreshToken =
            await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.UserId.Equals(id) &&
                                          x.Token.Equals(refreshTokenFromRequest) &&
                                          x.IsRevoked == false && 
                                          DateTime.UtcNow < x.Expires);
        if (refreshToken is null) return new RecordUserResponseDto(false, new UserModel(), "Token not found");
        refreshToken.IsRevoked = true;
        await _context.SaveChangesAsync();
        return new RecordUserResponseDto(true, new UserModel(), "Log out successful");
    }

    public async Task<LoginResponseDto> RefreshToken(string refreshTokenFromRequest) {
        var refreshToken =
            await _context.RefreshTokens
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x =>
                                          x.Token.Equals(refreshTokenFromRequest) &&
                                          x.IsRevoked == false && 
                                          DateTime.UtcNow < x.Expires);
        if (refreshToken is null) 
            return new LoginResponseDto(false, "", "Token not found");
        return new LoginResponseDto(true, "Get new access token successfully",
            _jwtService.CreateAssessToken(refreshToken.User));
    }

    public async Task<RecordUserResponseDto> GetUser(Guid id) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return user is null
            ? new RecordUserResponseDto(false, new UserModel(), "Not found user")
            : new RecordUserResponseDto(true, user, "Find user successful");
    }

    public async Task<RecordUserResponseDto> UpdateUser(UpdateUserDto.UpdateInfoDto userDto ,Guid id) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (user is null)
            return new RecordUserResponseDto(false, new UserModel(), "Not found user");
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.DateOfBirth = userDto.DateOfBirth;
        await _context.SaveChangesAsync();
        return new RecordUserResponseDto(true, user, "Update info of user successful");;
    }

    public Task<RecordUserResponseDto> FindUser(string name) {
        throw new NotImplementedException();
    }

    public async Task<RecordUserResponseDto> UpdateAvatar(UpdateUserDto.UpdateAvatarDto avatarDto, Guid id) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (user is null) 
            return new RecordUserResponseDto(false, new UserModel(), "Not found user");
        user.Avatar = avatarDto.AvatarUrl;
        await _context.SaveChangesAsync();
        return new RecordUserResponseDto(true, user, "Update info of user successful");;
    }

    public async Task<RecordUserResponseDto> UpdatePassword(UpdateUserDto.UpdatePasswordDto passwordDto, Guid id) {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (user is null) 
            return new RecordUserResponseDto(false, new UserModel(), "Not found user");
        if (!PasswordHasher.IsCorrectPassword(passwordDto.OldPassword, user.Password))
            return new RecordUserResponseDto(false, new UserModel(), "Old password is incorrect");
        user.Password = PasswordHasher.HashPasswordUser(passwordDto.NewPassword);
        await _context.SaveChangesAsync();
        return new RecordUserResponseDto(true, user, "Update password successful");
    }
}