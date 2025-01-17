using ChatApp.Application.Dtos.Requests.Room;
using ChatApp.Application.Dtos.Responses.Room;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using MediatR;

namespace ChatApp.Application.Commands.Room;

public record CreateCommand(CreateRoomDto CreateRoomDto, Guid OwnerId) : IRequest<RecordRoomResponseDto>;

public class CreateCommandHandler(IRoomRepository roomRepository) : IRequestHandler<CreateCommand, RecordRoomResponseDto> {
    public async Task<RecordRoomResponseDto> Handle(CreateCommand request, CancellationToken cancellationToken) {
        return await roomRepository.CreateRoom(request.CreateRoomDto, request.OwnerId);
    }
}