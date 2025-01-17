using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Models;

namespace ChatApp.Application.Dtos.Responses.Message;

public class MessageResponseDto {
    public string Content {get;set;}
    public Guid Id { get; set; }
    public string Avatar { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CreatedAt { get; set; } 
}

public record MessageResponseRecord(bool Flag,MessageModel MessageModel,string Message=null!);