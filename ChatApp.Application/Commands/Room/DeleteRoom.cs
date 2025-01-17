using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.Room;

public record DeleteRoomCommand(Guid RoomId, Guid IdUser) : IRequest<RecordRoomResponseDto>;

public class DeleteRoomCommandHandler(IRoomRepository roomRepository) : IRequestHandler<DeleteRoomCommand, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(DeleteRoomCommand request, CancellationToken cancellationToken) {
        return await roomRepository.DeleteRoom(request.RoomId, request.IdUser);
    }
}