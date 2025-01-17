namespace ChatApp.Application.Dtos.Responses.User;

public record LoginResponseDto(bool Flag,
    string Message = null!, string AccessToken = null!, string RefreshToken = null!);