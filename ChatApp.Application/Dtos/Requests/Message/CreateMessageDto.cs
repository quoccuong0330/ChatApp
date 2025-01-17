using Enum = ChatApp.Core.Enums.Enum;

namespace ChatApp.Application.Dtos.Requests.Message;

public class CreateMessageDto {
    public string Content { get; set; } = string.Empty;
    public Enum.MessageType MessageType { get; set; }
    public Guid RoomId { get; set; }
}
