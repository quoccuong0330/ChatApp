using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.Models;

public class SeenMessageModel {
    [Key]
    public Guid RoomId { get; set; } 
    public ChatRoomModel Room { get; set; } 
    public Guid MessageId { get; set; } 
    public MessageModel Message { get; set; } 
    public Guid UserId { get; set; } 
    public UserModel User { get; set; } 

    public DateTime SeenAt { get; set; }
}