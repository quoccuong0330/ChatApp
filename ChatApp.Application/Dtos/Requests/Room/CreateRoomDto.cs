namespace ChatApp.Application.Dtos.Requests.Room;

public class CreateRoomDto {
    public ICollection<Guid> members { get; set; }
}