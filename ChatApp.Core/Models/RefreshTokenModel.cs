using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.Models;

public class RefreshTokenModel {
        [Key]
        public Guid Id { get; set; } 
        public string Token { get; set; } = Guid.NewGuid().ToString();  
        public DateTime Expires { get; set; } 
        public bool IsRevoked { get; set; } = false;  
        public bool IsExpired => DateTime.UtcNow >= Expires;  

        public Guid UserId { get; set; } 
        public UserModel User { get; set; } = null!; 
}