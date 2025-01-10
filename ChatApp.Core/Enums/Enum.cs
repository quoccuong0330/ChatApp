namespace ChatApp.Core.Enums;

public class Enum {
    public enum RoomType {
        Group = 0,
        Private = 1
    }
    public enum MessageStatus {
        Sending = 0,
        Sent = 1,
        Seen = 2,
        Failed = 3
    }

    public enum MessageType {
        Text = 0,
        Image = 1,
        Video = 2,
        File=3,
        Other=4
    }
    
    public enum RoleInRoom {
        Member = 0,
        Admin = 1,
        
    }
}