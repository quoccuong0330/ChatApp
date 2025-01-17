namespace ChatApp.Application.Dtos.Requests.User;

public class LogoutDto {
    public string RefreshToken { get; set; } = string.Empty;
}