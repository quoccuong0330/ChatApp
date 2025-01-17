using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.Room;

public record UpdateRoomNameCommand(Guid RoomId, string Name, Guid IdUser) : IRequest<RecordRoomResponseDto>;

public class UpdateRoomNameCommandHandler(IRoomRepository roomRepository) : IRequestHandler<UpdateRoomNameCommand, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(UpdateRoomNameCommand request, CancellationToken cancellationToken) {
        return await roomRepository.UpdateName(request.RoomId, request.Name, request.IdUser);
    }
}