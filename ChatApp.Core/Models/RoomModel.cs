using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.Models;

public class RoomModel {
    [Key]
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public ICollection<RoomMemberModel> RoomMembers { get; set; }
    public ICollection<MessageModel?> Messages { get; set; }
    public UserModel Owner { get; set; }
}