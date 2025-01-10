using System.ComponentModel.DataAnnotations;
using Enum = ChatApp.Core.Enums.Enum;

namespace ChatApp.Core.Models;


public class ChatRoomModel {
    [Key]
    public Guid IdRoom  {get;set;}
    [Required]
    public Enum.RoomType Type{ get; set; }
    public Guid? LastMessageId { get; set; }  
    public MessageModel LastMessage { get; set; } 
    [Required]
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    [Required]
    public DateTime UpdatedAt  {get;set;}
    
    public ICollection<UserChatRoomModel> UserChatRooms { get; set; } =new List<UserChatRoomModel>();
    public ICollection<MessageModel> Messages { get; set; } =new List<MessageModel>();
    public ICollection<SeenMessageModel> SeenMessages { get; set; } =new List<SeenMessageModel>();
}