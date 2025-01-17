using System.Security.Cryptography;

namespace ChatApp.Application.Dtos.Requests.Room;

public class LeaveRoomDto {
    public Guid idRoom{get;set;}
    public Guid idUser{get;set;}
}