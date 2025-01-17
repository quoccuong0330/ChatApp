using System.ComponentModel.DataAnnotations;

namespace ChatApp.Application.Dtos.Requests.User;

public class LoginDto {
    [Required]
    [EmailAddress]
    public string Email {get;set;}
    [Required]
    [MinLength(6,ErrorMessage = "Password flied is not less than 6 characters.")]
    public string Password {get;set;}
}