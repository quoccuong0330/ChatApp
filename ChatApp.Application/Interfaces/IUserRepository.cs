using ChatApp.Application.Dtos.Requests.User;
using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces;

public interface IUserRepository {
    public Task<RecordUserResponseDto> Register(UserModel userModel);
    public Task<LoginResponseDto> Login(LoginDto loginDto);
    public Task<RecordUserResponseDto> LogOut(Guid id,string refreshTokenFromRequest);
    public Task<LoginResponseDto> RefreshToken(string refreshTokenFromRequest);
    public Task<RecordUserResponseDto> GetUser(Guid id);
    public Task<RecordUserResponseDto> UpdateUser(UpdateUserDto.UpdateInfoDto userDto, Guid id);
    public Task<RecordUserResponseDto> FindUser(string name);
    Task<RecordUserResponseDto> UpdateAvatar(UpdateUserDto.UpdateAvatarDto avatarDto, Guid userId);
    Task<RecordUserResponseDto> UpdatePassword(UpdateUserDto.UpdatePasswordDto passwordDto, Guid userId);
}