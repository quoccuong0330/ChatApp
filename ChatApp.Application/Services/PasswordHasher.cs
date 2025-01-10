namespace ChatApp.Application.Services;

public static class PasswordHasher {
    public static string HashPasswordUser(string password) {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool IsCorrectPassword(string password,string hashPassword) {
        return BCrypt.Net.BCrypt.Verify(password,hashPassword);
    }
}