using System.ComponentModel.DataAnnotations;
using Enum = ChatApp.Core.Enums.Enum;

namespace ChatApp.Core.Models;



public class UserChatRoomModel {
    [Key] public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; }

    public Guid RoomId { get; set; }
    public ChatRoomModel ChatRoom { get; set; }

    public DateTime JoinedAt { get; set; } 
    public Enum.RoleInRoom Role { get; set; }     
}