using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Queries.Room;

public record GetDetailRoom(Guid RoomId, Guid IdUser) : IRequest<RecordRoomResponseDto>;

public class GetDetailRoomQueryHandler(IRoomRepository roomRepository) : IRequestHandler<GetDetailRoom, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(GetDetailRoom request, CancellationToken cancellationToken) {
        return await roomRepository.GetDetailRoom(request.RoomId, request.IdUser);
    }
}