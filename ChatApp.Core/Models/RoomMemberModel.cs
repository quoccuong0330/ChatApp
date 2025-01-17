using System.ComponentModel.DataAnnotations;
using Enum = ChatApp.Core.Enums.Enum;

namespace ChatApp.Core.Models;


public class RoomMemberModel {
    [Key]
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime JoinedAt { get; set; }

    // Navigation Properties
    public RoomModel Room { get; set; }
    public UserModel User { get; set; }
}