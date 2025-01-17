using System.ComponentModel.DataAnnotations;
using Enum = ChatApp.Core.Enums.Enum;

namespace ChatApp.Core.Models;


public class MessageModel {
    [Key]
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Enum.MessageType MessageType { get; set; }
    public Guid UserIdCreate { get; set; }
    public Guid RoomId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public UserModel User { get; set; }
    public RoomModel Room { get; set; }
}