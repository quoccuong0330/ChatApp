using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.Models;

public class UserModel {
    [Key]
    public Guid Id { get; set; }
       [Required(ErrorMessage = "Email is required")]
    [MaxLength(50, ErrorMessage = "Not more than 50 characters")]
    public string FirstName { get; set; } = string.Empty;
       [Required(ErrorMessage = "Email is required")]
    [MaxLength(50, ErrorMessage = "Not more than 50 characters")]
    public string LastName{ get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email{ get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.Password)]
    public string Password{ get; set; } = string.Empty;
    public string Avatar{ get; set; } = string.Empty;
    public DateTime DateOfBirth{ get; set; }
    
    public ICollection<UserChatRoomModel> UserChatRooms { get; set; } = new List<UserChatRoomModel>();
    public ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
    public ICollection<SeenMessageModel> SeenMessages { get; set; } = new List<SeenMessageModel>();
}