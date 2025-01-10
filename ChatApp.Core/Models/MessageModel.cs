using System.ComponentModel.DataAnnotations;
using Enum = ChatApp.Core.Enums.Enum;

namespace ChatApp.Core.Models;




public class MessageModel {
    [Key]
    public Guid Id {get;set;}
    [Required]
    public Guid RoomId {get;set;}
    public ChatRoomModel Room { get; set; }
    [Required]
    public Guid SenderId {get;set;}
    public UserModel Sender { get; set; }
    public string Message { get; set; } = string.Empty;
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Enum.MessageType Type { get; set; }
    public bool IsDelete { get; set; } = false;
    public Enum.MessageStatus Status { get; set; } = Enum.MessageStatus.Sending;
    public ICollection<SeenMessageModel> SeenMessages { get; set; } = new List<SeenMessageModel>();

}