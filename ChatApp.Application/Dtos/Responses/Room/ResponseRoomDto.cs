using ChatApp.Application.Dtos.Responses.User;

namespace ChatApp.Application.Dtos.Responses.Room;

public class CreateResponseDto {
    public Guid Id {get;set;}
    public string Name {get;set;}
    public string Avatar {get;set;}
    public ICollection<ResponseUserDto> Users {get;set;}
}

