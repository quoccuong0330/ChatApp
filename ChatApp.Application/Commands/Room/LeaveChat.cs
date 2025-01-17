using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.Room;

public record LeaveChatCommand(Guid RoomId,Guid UserId) : IRequest<RecordRoomResponseDto>;

public class LeaveChatCommandHandler(IRoomRepository roomRepository) : IRequestHandler<LeaveChatCommand, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(LeaveChatCommand request, CancellationToken cancellationToken) {
        return await roomRepository.LeaveRoom(request.RoomId, request.UserId);
    }
}