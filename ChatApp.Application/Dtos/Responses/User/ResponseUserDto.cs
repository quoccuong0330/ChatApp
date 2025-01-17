using System.ComponentModel.DataAnnotations;
using ChatApp.Core.Models;

namespace ChatApp.Application.Dtos.Responses.User;

public class ResponseUserDto {
    public Guid Id { get; set; }
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get;set;}
    public string Avatar {get;set;}
    public string DateOfBirth {get;set;}
}

public record RecordUserResponseDto(bool Flag, UserModel UserModel=null!, string Message = null!);