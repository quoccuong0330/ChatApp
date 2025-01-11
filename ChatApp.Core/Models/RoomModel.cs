using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.Models;

public class Room {
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation Properties
    public ICollection<GroupMember> GroupMembers { get; set; }
    public ICollection<Message> Messages { get; set; }
    public User Owner { get; set; }
}