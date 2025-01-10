using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Application.Dtos.Requests.User;

public class RegisterUserDto {
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get;set;}
    public string Password {get;set;}
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [NotMapped]
    public string ConfrimPassword {get;set;}
    public string Avatar {get;set;}
    public DateTime DateOfBirth {get;set;}
}