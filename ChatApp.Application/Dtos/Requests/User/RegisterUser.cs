using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Application.Dtos.Requests.User;

public class RegisterUserDto {
    [Required]
    public string FirstName {get;set;}
    [Required]
    public string LastName {get;set;}
    [Required]
    [EmailAddress]
    public string Email {get;set;}
    [Required]
    [MinLength(6,ErrorMessage = "Password flied is not less than 6 characters.")]
    public string Password {get;set;}
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [NotMapped]
    public string ConfrimPassword {get;set;}
    public string Avatar {get;set;}
    public DateTime DateOfBirth {get;set;}
}