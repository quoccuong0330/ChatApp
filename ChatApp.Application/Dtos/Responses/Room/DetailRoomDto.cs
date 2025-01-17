using ChatApp.Application.Dtos.Responses.Message;

namespace ChatApp.Application.Dtos.Responses.Room;

public class DetailRoomDto {
    public Guid Id;
    public string Name;
    public string Avatar;
    public ICollection<MessageResponseDto> Messages;
}