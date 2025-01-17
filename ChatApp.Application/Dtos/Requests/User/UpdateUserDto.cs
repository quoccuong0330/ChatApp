using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Application.Dtos.Responses.User;

public class UpdateUserDto {
    public class UpdateInfoDto{
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public DateTime DateOfBirth {get;set;}
    }

    public class UpdateAvatarDto {
        public string AvatarUrl {get;set;}
    }
    
    public class UpdatePasswordDto {
        [Required]
        public string OldPassword {get;set;}
        [Required]
        public string NewPassword {get;set;}
        [Required]
        [Compare("NewPassword")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}