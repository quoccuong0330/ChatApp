using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.Models;

public class UserModel {
    [Key]
    public Guid Id { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName{ get; set; } = string.Empty;
    public string Email{ get; set; } = string.Empty;
    public string Password{ get; set; } = string.Empty;
    public string Avatar{ get; set; } = string.Empty;
    public DateTime DateOfBirth{ get; set; }
    
    public ICollection<RoomMemberModel> RoomMembers { get; set; }
    public ICollection<MessageModel> Messages { get; set; }
    public ICollection<RefreshTokenModel> RefreshTokens { get; set; }
}