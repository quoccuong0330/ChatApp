using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.Room;

public record UpdateRoomAvatarCommand(Guid RoomId, string Avatar, Guid IdUser) : IRequest<RecordRoomResponseDto>;

public class UpdateRoomAvatarCommandHandler(IRoomRepository roomRepository) : IRequestHandler<UpdateRoomAvatarCommand, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(UpdateRoomAvatarCommand request, CancellationToken cancellationToken) {
        return await roomRepository.UpdateAvatar(request.RoomId, request.Avatar, request.IdUser);
    }
}