using ChatApp.Application.Dtos.Responses.Message;
using ChatApp.Core.Models;

namespace ChatApp.Application.Dtos.Responses.Room;

public class ListResponseDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public MessageResponseDto? LastMessage { get; set; } // Nullable if no messages exist
}

public record ListRoomRecordDto(bool Flag,
    ICollection<RoomModel> Rooms, string Message = null!);