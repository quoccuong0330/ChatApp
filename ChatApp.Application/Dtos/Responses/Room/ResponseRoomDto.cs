using ChatApp.Application.Dtos.Responses.User;
using ChatApp.Core.Models;

namespace ChatApp.Application.Dtos.Responses.Room;

public class ResponseRoomDto {
    public Guid Id {get;set;}
    public string Name {get;set;}
    public string Avatar {get;set;}
    public DateTime CreatedAt { get; set; }
    public ResponseUserDto Owner { get; set; }
    public ICollection<ResponseUserDto> Users {get;set;}
}

public record RecordRoomResponseDto(bool Flag, RoomModel RoomModel=null!, string Message = null!);
